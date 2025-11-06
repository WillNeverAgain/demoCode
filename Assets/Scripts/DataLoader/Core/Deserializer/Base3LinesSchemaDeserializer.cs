//Author : _SourceCode
//CreateTime : 2025-11-05-17:40:12
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System;
using System.Collections.Generic;
using System.Numerics;

namespace MyFrame.DataManager
{
    public class Base3LinesSchemaDeserializer : ISchemaDeserializer
    {
        private Dictionary<string, Type> keyValuePairs = new Dictionary<string, Type>()
        {
            {"string",typeof(string) },
            {"int",typeof(int) },
            {"float",typeof(float) },
            {"bool",typeof(bool) },
            { "int_arry",typeof(int[])},
            {"vector2",typeof(Vector2) },
            {"vector3",typeof(Vector3) }
        };
        public int GetNeedDeserializerLines()
        {
            return 3;
        }

        public bool TryDeserializerSchema(List<string[]> lines, out TableSchema schema, out string[] keys)
        {
            int len = 0;
            schema = null;
            keys = null;
            Dictionary<string,TableColumn> dic = new Dictionary<string, TableColumn>();
            for ( int i = 0; i < lines.Count; i++ )
            {
                string[] line = lines[i];
                if (line is null || line.Length == 0) return false;
                if (len == 0) len = line.Length;
                else if (len != line.Length) return false;
            }
            keys = new string[len];
            for (int i = 0; i <len ; i++)
            {
                var typeToken = (lines[2][i] ?? "string").Trim().ToLowerInvariant();
                if (!keyValuePairs.TryGetValue(typeToken, out var type)) return false;

                var alias = lines[1][i]?.Trim();
                if (string.IsNullOrEmpty(alias)) return false;

                var comment = (lines[0][i] ?? string.Empty).Trim();
                TableColumn c = new TableColumn($"策划备注:{comment} 别名:{alias}", type);
                if (!dic.TryAdd(alias, c)) { return false; }
                keys[i] = alias;
            }
            schema = new TableSchema(dic);
            return true;

        }
    }
}
