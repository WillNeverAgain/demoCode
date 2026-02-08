//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System;
using System.IO;
using MyFrame.FightSystem.SkillV2.Configs;
using UnityEngine;

namespace MyFrame.FightSystem.SkillV2.Serialization
{
    /// <summary>
    /// JSON <-> SkillConfigData utilities.
    /// 
    /// Runtime recommendation:
    /// - Put exported json under StreamingAssets/SkillV2/skills/*.json
    /// - Load with <see cref="LoadFromStreamingAssets"/>.
    /// 
    /// NOTE:
    /// - Uses UnityEngine.JsonUtility (fast, AOT friendly). It supports nested serializable classes and Lists.
    /// - Avoid Dictionary / polymorphic fields in configs if you want JsonUtility compatibility.
    /// </summary>
    public static class SkillV2Json
    {
        /// <summary>Deserialize from json text.</summary>
        public static SkillConfigData LoadFromJsonText(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("json is null or empty", nameof(json));

            var cfg = JsonUtility.FromJson<SkillConfigData>(json);
            if (cfg is null)
                throw new InvalidOperationException("JsonUtility returned null config. Check json format.");

            return cfg;
        }

        /// <summary>Serialize to json text.</summary>
        public static string ToJsonText(SkillConfigData cfg, bool prettyPrint = true)
        {
            if (cfg is null) throw new ArgumentNullException(nameof(cfg));
            return JsonUtility.ToJson(cfg, prettyPrint);
        }

        /// <summary>
        /// Load from an absolute file path.
        /// Useful in editor or standalone builds.
        /// </summary>
        public static SkillConfigData LoadFromFile(string absolutePath)
        {
            if (string.IsNullOrWhiteSpace(absolutePath))
                throw new ArgumentException("path is null or empty", nameof(absolutePath));

            var json = File.ReadAllText(absolutePath);
            return LoadFromJsonText(json);
        }

        /// <summary>
        /// Save to an absolute file path (creates parent folders if needed).
        /// </summary>
        public static void SaveToFile(SkillConfigData cfg, string absolutePath, bool prettyPrint = true)
        {
            if (cfg is null) throw new ArgumentNullException(nameof(cfg));
            if (string.IsNullOrWhiteSpace(absolutePath))
                throw new ArgumentException("path is null or empty", nameof(absolutePath));

            var dir = Path.GetDirectoryName(absolutePath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(absolutePath, ToJsonText(cfg, prettyPrint));
        }

        /// <summary>
        /// Load from StreamingAssets (relative path inside StreamingAssets).
        /// Example: relativePath = "SkillV2/skills/skill_fireball.json"
        /// </summary>
        public static SkillConfigData LoadFromStreamingAssets(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                throw new ArgumentException("relativePath is null or empty", nameof(relativePath));

            var full = Path.Combine(Application.streamingAssetsPath, relativePath);
            return LoadFromFile(full);
        }

        /// <summary>
        /// Load from Resources (TextAsset). Example path: "SkillV2/skills/skill_fireball"
        /// </summary>
        public static SkillConfigData LoadFromResources(string resourcesPath)
        {
            if (string.IsNullOrWhiteSpace(resourcesPath))
                throw new ArgumentException("resourcesPath is null or empty", nameof(resourcesPath));

            var ta = Resources.Load<TextAsset>(resourcesPath);
            if (ta is null)
                throw new FileNotFoundException($"Resources TextAsset not found at: {resourcesPath}");

            return LoadFromJsonText(ta.text);
        }
    }
}
