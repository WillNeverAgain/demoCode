//Author : _SourceCode
//CreateTime : 2025-10-30-22:44:12
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System;
using System.Collections.Generic;
using System.Text;

namespace MyFrame.DataManager
{
    /// <summary>
    /// 基础类型解析器
    /// </summary>
    public interface IBaseClassDeserializer
    {
        public ReadOnlyMemory<char> DeserializerBaseClass(string baseData, string schema);
    }
    /// <summary>
    /// 加载器
    /// </summary>
    public interface IDataLoader
    {
        /// <summary>
        /// 进行加载(同步)
        /// </summary>
        /// <param name="command">加载命令参数</param>
        /// <param name="deserializer">反序列化器</param>
        /// <param name="data">接收信息用的table</param>
        /// <returns></returns>
        public bool Load<TTable, TRow>(LoadCommand command, IDataDeserializer<TRow> deserializer, TTable data) where TTable : IDataTable<TRow>;
    }
    /// <summary>
    /// 反序列化器，用于将读取的信息行转为自定义的信息类
    /// </summary>
    public interface IDataDeserializer<T>
    {
        /// <summary>
        /// 自定义解析函数
        /// </summary>
        /// <param name="origin">每行的原始数据，依据command中给出的切分符进行切分</param>
        /// <param name="row">需要转换为的数据类</param>
        /// <returns>是否解析成功</returns>
#   nullable enable
        public bool TryDeserializer(IReadOnlyList<ReadOnlyMemory<char>> origin, out T row , out string? err);
#   nullable disable
    }

    /// <summary>
    /// 信息接收table
    /// </summary>
    public interface IDataTable<T>
    {
        /// <summary>
        /// 开始加载时行为
        /// </summary>
        /// <param name="schema">表头</param>
        public void BeginLoad(TableSchema schema);
        /// <summary>
        /// 加入新读取的行
        /// </summary>
        /// <param name="row">新加入的行数据</param>
        public void AddRow(ref T row);
        /// <summary>
        /// 结束读取时的行为
        /// </summary>
        /// <param name="summary">读取结束时的总览信息</param>
        public void EndLoad(LoadSummary summary);
    }
    /// <summary>
    /// 读取结束时的总览信息
    /// </summary>
    /// <param name="RowCount">总行数</param>
    /// <param name="Duration">总读取时间</param>
    /// <param name="ErrorCount">读取错误的行数</param>
    public record LoadSummary(int RowCount, TimeSpan Duration, int ErrorCount);
    /// <summary>
    /// 加载命令参数
    /// </summary>
    public sealed class LoadCommand
    {
        /// <summary>
        /// 加载路径
        /// </summary>
        public string path {  get; init; }
        /// <summary>
        /// 解析表头策略接口
        /// </summary>
        public ISchemaDeserializer schemaDeserializer {  get; init; } = new Base3LinesSchemaDeserializer();
        ///// <summary>
        ///// 分隔符
        ///// </summary>
        //public char Delimiter { get; init; } = ',';
        /// <summary>
        /// 编码格式，默认为UTF8
        /// </summary>
        public Encoding encoding { get; init; } = Encoding.UTF8;
    }
    /// <summary>
    /// 解析表头策略接口
    /// </summary>
    public interface ISchemaDeserializer
    {
        public int GetNeedDeserializerLines();
        public bool TryDeserializerSchema(List<string[]> lines,out TableSchema schema);
    }
    /// <summary>
    /// 表头
    /// </summary>
    /// <param name="Columns">列信息</param>
    public record TableSchema(IReadOnlyList<TableColumn> Columns);
    /// <summary>
    /// 表头列信息
    /// </summary>
    /// <param name="Name">列名</param>
    /// <param name="Type">类型</param>
    /// <param name="Nullable">是否可为空</param>
    public record TableColumn(string Name, Type Type, bool Nullable = true);
}
