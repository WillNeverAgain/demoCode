//Author : _SourceCode
//CreateTime : 2025-10-31-15:16:35
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System;
using System.Globalization;
using System.Numerics;

namespace MyFrame.DataManager
{
    public static class BaseClassDeserializer
    {
        public static bool TryParse(ReadOnlyMemory<char> input, Type targetType, out object value)
        {
            var s = input.Span.Trim();
            var inv = CultureInfo.InvariantCulture;

            switch (targetType)
            {
                case var t when t == typeof(string):
                    value = s.ToString();
                    return true;

                case var t when t == typeof(int):
                    if (int.TryParse(s, NumberStyles.Integer, inv, out var i))
                    { value = i; return true; }
                    break;

                case var t when t == typeof(float):
                    if (float.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, inv, out var f))
                    { value = f; return true; }
                    break;

                case var t when t == typeof(bool):
                    if (bool.TryParse(s, out var b)) 
                    { value = b; return true; }
                    break;

                case var t when t == typeof(int[]):
                    {
                        var text = s.ToString();
                        if (text.Length == 0) { value = Array.Empty<int>(); return true; }
                        var parts = text.Split('=', StringSplitOptions.RemoveEmptyEntries);
                        var arr = new int[parts.Length];
                        for (int k = 0; k < parts.Length; k++)
                            if (!int.TryParse(parts[k], NumberStyles.Integer, inv, out arr[k]))
                                goto fail;
                        value = arr; 
                        return true;
                    }

                case var t when t == typeof(Vector2):
                    if (TryVec2(s.ToString(), out var v2)) { value = v2; return true; }
                    break;

                case var t when t == typeof(Vector3):
                    if (TryVec3(s.ToString(), out var v3)) { value = v3; return true; }
                    break;
            }

        fail:
            value = null;
            return false;

            static bool TryVec2(string text, out Vector2 v)
            {
                var p = text.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (p.Length == 2 &&
                    float.TryParse(p[0], NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var x) &&
                    float.TryParse(p[1], NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var y))
                { v = new Vector2(x, y); return true; }
                v = default; return false;
            }

            static bool TryVec3(string text, out Vector3 v)
            {
                var p = text.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (p.Length == 3 &&
                    float.TryParse(p[0], NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var x) &&
                    float.TryParse(p[1], NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var y) &&
                    float.TryParse(p[2], NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var z))
                { v = new Vector3(x, y, z); return true; }
                v = default; return false;
            }
        }
    }
}
