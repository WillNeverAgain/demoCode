//Author : _SourceCode
//CreateTime : 2025-11-05-17:40:12
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFrame.DataManager
{
    public class Base3LinesSchemaDeserializer : ISchemaDeserializer
    {
        private Dictionary<string, Type> keyValuePairs = new Dictionary<string, Type>()
        {
            {"string",typeof(string) },
            {"int",typeof(int) },
            {"long",typeof(long) },
            {"float",typeof(float) },
            {"double",typeof(double) },
            {"bool",typeof(bool) },
            { "int_arry",typeof(int[])}
        };
        public int GetNeedDeserializerLines()
        {
            return 3;
        }

        public bool TryDeserializerSchema(List<string[]> lines, out TableSchema schema)
        {
            int len = 0;
            schema = null;
            for ( int i = 0; i < lines.Count; i++ )
            {
                string[] line = lines[i];
                if (line is null || line.Length == 0) return false;
                if (len == 0) len = line.Length;
                else if (len != line.Length) return false;
            }
            for (int i = 0; i <len ; i++)
            {
                if (!keyValuePairs.TryGetValue(lines[2][i] ?? "string",out Type type)) return false;
                TableColumn c = new TableColumn("策划备注: " + lines[0][i] + "别名: " + lines[1][i] , type);
            }
            return true;

        }
    }
}
