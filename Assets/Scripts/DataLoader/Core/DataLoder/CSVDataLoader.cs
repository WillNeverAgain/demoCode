//Author : _SourceCode
//CreateTime : 2025-10-31-15:16:35
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyFrame.DataManager
{
    public sealed record LoadProgress(int Rows, int Lines, long BytesRead, long TotalBytes)
    {
        public double Ratio => TotalBytes <= 0 ? 0 : (double)BytesRead / TotalBytes;
    }

    public sealed record LoadResult(DataContainer Data, LoadSummary Summary, IReadOnlyList<string> Errors);

    public class CSVDataLoader : IDataLoader
    {
        private const char _delimiter = ',';
        public async Task<LoadResult> LoadAsync(
            LoadCommand command,
            IProgress<LoadProgress>? progress = null,
            CancellationToken ct = default
        )
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var errors = new List<string>();
            int rowCount = 0;

            var data = new DataContainer();
            var _schemaDeserializer = command.schemaDeserializer;

            // 异步文件打开
            using var fs = new FileStream(
                command.path,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 64 * 1024,
                options: FileOptions.Asynchronous);

            using var sr = new StreamReader(
                fs, command.encoding ?? Encoding.UTF8, detectEncodingFromByteOrderMarks: true);

            // 读取表头（异步逐行）
            List<string[]> head = new();
            for (int i = 0; i < _schemaDeserializer.GetNeedDeserializerLines(); i++)
            {
                ct.ThrowIfCancellationRequested();
                string? line = await sr.ReadLineAsync().ConfigureAwait(false);
                if (line is null)
                    throw new InvalidDataException("Unexpected EOF while reading header.");
                head.Add(SplitCsv(line, _delimiter));
            }

            // 解析表头
            if (!_schemaDeserializer.TryDeserializerSchema(head, out TableSchema schema, out string[] keys))
                throw new InvalidDataException("Schema deserialization failed.");
            data.BeginLoad(schema);

            string? data_line;
            int lineNo = _schemaDeserializer.GetNeedDeserializerLines();
            long totalBytes = fs.Length;

            while ((data_line = await sr.ReadLineAsync().ConfigureAwait(false)) is not null)
            {
                ct.ThrowIfCancellationRequested();

                if (data_line.Length == 0) { lineNo++; Report(); continue; }

                string[] fields = SplitCsv(data_line, _delimiter);

                // 列数防御
                if (fields.Length < keys.Length)
                {
                    errors.Add($"Row {lineNo}: ERR: line data num {fields.Length} is less than schema num {keys.Length}");
                    lineNo++; Report(); continue;
                }

                ReadOnlyMemory<char>[] mem = Array.ConvertAll(fields, f => (ReadOnlyMemory<char>)f.AsMemory());
                Dictionary<string, object> dic = new();

                bool no_err = true;
                // 解析行数据
                for (int i = 0; i < keys.Length; i++)
                {
                    if (!schema.Columns.TryGetValue(keys[i], out var col))
                    { errors.Add($"Row {lineNo}: Column : {keys[i]} ERR: Schema can not get value"); no_err = false; break; }

                    if (!BaseClassDeserializer.TryParse(mem[i], col.Type, out var value))
                    { errors.Add($"Row {lineNo}: Column : {keys[i]} ERR: BaseClassDeserializer.TryParse {mem[i].Span.ToString()} TO {col.Type} Failed"); no_err = false; break; }

                    if (!dic.TryAdd(keys[i], value))
                    { errors.Add($"Row {lineNo}: Column : {keys[i]} ERR: Dictionary Try Add ({keys[i]} , {value}) Failed"); no_err = false; break; }
                }

                if (no_err)
                {
                    var temp_dic = new DataContainerColumn() { dictionary = dic };
                    if (data.TryAddRow(mem[0].Span.ToString(), ref temp_dic)) { rowCount++; }
                    else { errors.Add($"Row {lineNo}: ERR: DataContainer Try Add Row Failed"); }
                }

                lineNo++;
                Report();
            }

            data.EndLoad(new LoadSummary(rowCount, sw.Elapsed, errors.Count));
            sw.Stop();

            return new LoadResult(data, data.LoadSummary, errors);

            // 内部进度上报
            void Report()
            {
                if (progress is null) return;
                progress.Report(new LoadProgress(rowCount, lineNo, fs.Position, totalBytes));
            }
        }

        private static string[] SplitCsv(string line, char delimiter)
        {
            var list = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        sb.Append('"'); i++; // 双引号转义
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == delimiter && !inQuotes)
                {
                    list.Add(sb.ToString()); sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }
            list.Add(sb.ToString());
            return list.ToArray();
        }
    }
}
