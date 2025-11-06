//Author : _SourceCode
//CreateTime : 2025-10-30-22:44:12
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System.Collections.Generic;

namespace MyFrame.DataManager
{
    public class DataContainer
    {
        private Dictionary<string, DataContainerColumn> dictionary = new Dictionary<string, DataContainerColumn>();
        private TableSchema _schema;
        public LoadSummary LoadSummary { get; private set; }
        public bool IsEnd {  get; private set; }


        public bool TryGet(string key, out DataContainerColumn value)
        {
            value = null;
            if (!dictionary.TryGetValue(key, out DataContainerColumn data)) return false;
            value = data;
            return true;
        }
        public bool TryGetValue<T>(string key_of_column, string field, out T value)
        {
            value = default(T);
            if (!dictionary.TryGetValue(key_of_column, out DataContainerColumn data)) return false;
            if(!data.TryGet<T>(field, out T true_data)) return false;
            value = true_data;
            return true;
        }
        public void BeginLoad(TableSchema schema)
        {
            _schema = schema;
        }
        public bool TryAddRow(string key,ref DataContainerColumn value)
        {
            if(!dictionary.TryAdd(key, value)) {  return false; }
            value.SetSchema(_schema);
            return true;
        }

        public void EndLoad(LoadSummary summary)
        {
            IsEnd = true;
            LoadSummary = summary;
        }
    }

    public class DataContainerColumn
    {
        public Dictionary<string, object> dictionary { get; init; } = new Dictionary<string, object>();
        private TableSchema _schema = null;
        public bool TryGet<T>(string key, out T value)
        {
            value = default(T);
            if (_schema is null || !_schema.Columns.TryGetValue(key, out TableColumn column) || column.Type != typeof(T)) return false;
            if (!dictionary.TryGetValue(key, out object data)) return false;
            value = (T)data;
            return true;
        }

        public bool SetSchema(TableSchema schema)
        {
            if(_schema is not null) return false;
            _schema = schema;
            return true;
        }
    }
}