//Author : _SourceCode
//CreateTime : 2025-10-31-15:16:35
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFrame.DataManager
{
    public class CSVDataLoder : IDataLoader
    {
        private const char _delimiter = ',';

        public bool Load<TTable, TRow>(LoadCommand command, IDataDeserializer<TRow> deserializer, TTable data) where TTable : IDataTable<TRow>
        {
            ISchemaDeserializer _schemaDeserializer = command.schemaDeserializer;
            // 开始计时
            var sw = System.Diagnostics.Stopwatch.StartNew();
            //错误信息储存表
            var errors = new List<string>();
            //记录行数
            int rowCount = 0;
            //打开文件
            using var fs = File.OpenRead(command.path);
            using var sr = new StreamReader(fs, command.encoding ?? Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            //表头源信息
            List<string[]> head = new List<string[]>();

            for (int i = 0; i < _schemaDeserializer.GetNeedDeserializerLines(); i++)
            {
                string line = sr.ReadLine();
                head.Add (SplitCsv(line, _delimiter));
            }
            //解析表头
            if(!_schemaDeserializer.TryDeserializerSchema(head, out TableSchema schema)) return false;

            string? data_line;

            int lineNo = _schemaDeserializer.GetNeedDeserializerLines();

            while ((data_line = sr.ReadLine()) is not null)
            {
                if (data_line.Length == 0) { lineNo++; continue; }

                string[] fields = SplitCsv(data_line, _delimiter);

                ReadOnlyMemory<char>[] mem = Array.ConvertAll(fields, f => (ReadOnlyMemory<char>)f.AsMemory());

                // 解析行数据
                if (deserializer.TryDeserializer(mem, out TRow row,out string? err))
                {
                    data.AddRow(ref row);
                    rowCount++;
                }
                else
                {
                    errors.Add($"Row {lineNo}: {err ?? "deserialize failed"}");
                }

                lineNo++;
            }

            data.EndLoad(new LoadSummary(rowCount, sw.Elapsed, errors.Count));
            sw.Stop();

            // LoadResult 通过广播播放
            //rowCount,errors
            return true;
        }

        public static string[] SplitCsv(string line, char delimiter)
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
