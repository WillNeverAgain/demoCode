//Author : _SourceCode
//CreateTime : 2025-09-17-15:24:43
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Unit
{
    public class StatCacheHashVersion : IStatCache
    {
#nullable enable
        private readonly int _maxCache;
        private Dictionary<StatCacheKey,object?> cache;
        public StatCacheHashVersion(int maxCache = 100)
        {
            cache = new Dictionary<StatCacheKey, object?>();
            _maxCache = maxCache;
        }
        public void Set<T>(StatSpec<T> spec, StatContext ctx, long version, StatResult<T> value)
        {
            var key = new StatCacheKey(((int)spec.attributeType), ctx.GetStableHash(), version);
            if(cache.ContainsKey(key)) { cache[key] = value; }
            else { cache.Add(key, value); }
            if(cache.Count >= _maxCache)
            {
                InvalidVersion(version);
            }
        }

        public bool TryGet<T>(StatContext ctx, StatSpec<T> spec, long version, out StatResult<T>? value)
        {
            var key = new StatCacheKey(((int)spec.attributeType), ctx.GetStableHash(), version);
            var res = cache.TryGetValue(key, out object? object_value);
            if(object_value == null) { value = default; }
            value = (StatResult<T>)object_value!;
            return res;
        }
        public void InvalidVersion(long version)
        {
            List<StatCacheKey> keys = new List<StatCacheKey>();
            foreach(var key in cache.Keys)
            {
                if(key.Version != version)
                    keys.Add(key);
            }
            foreach(var key in keys)
            {
                cache.Remove(key);
            }
        }
    }
    public readonly struct StatCacheKey : IEquatable<StatCacheKey>
    {
        public readonly int SpecId;   // 属性ID（稳定的 AttributeId）
        public readonly int CtxHash;  // 上下文稳定哈希
        public readonly long Version;  // 版本钟（全局或按属性）

        public StatCacheKey(int specId, int ctxHash, long version)
        {
            SpecId = specId;
            CtxHash = ctxHash;
            Version = version;
        }

        public bool Equals(StatCacheKey other)
            => SpecId == other.SpecId && CtxHash == other.CtxHash && Version == other.Version;

        public override bool Equals(object obj)
            => obj is StatCacheKey o && Equals(o);

        public override int GetHashCode()
        {
            return HashCode.Combine(SpecId, CtxHash, Version);
        }
    }
}
