//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
#if UNITY_EDITOR
using System.IO;
using MyFrame.FightSystem.SkillV2.Configs;
using MyFrame.FightSystem.SkillV2.Serialization;
using UnityEditor;
using UnityEngine;

namespace MyFrame.FightSystem.SkillV2.Editor
{
    /// <summary>
    /// Editor helper: export a SkillConfigSO asset into JSON file.
    /// This is optional. The recommended authoring flow is:
    /// Excel -> JSON -> Runtime load.
    /// But SO export is still useful for quick debugging in Unity.
    /// </summary>
    public static class SkillV2ConfigExporter
    {
        [MenuItem("Tools/FightSystem/SkillV2/Export Selected SkillConfigSO To JSON")]
        public static void ExportSelected()
        {
            var so = Selection.activeObject as SkillConfigSO;
            if (so is null)
            {
                EditorUtility.DisplayDialog("SkillV2 Export", "Select a SkillConfigSO asset first.", "OK");
                return;
            }

            var data = SkillV2ConfigUtil.CloneToData(so);

            var path = EditorUtility.SaveFilePanel("Export SkillV2 JSON", Application.dataPath, so.skillId, "json");
            if (string.IsNullOrWhiteSpace(path)) return;

            SkillV2Json.SaveToFile(data, path, prettyPrint: true);
            EditorUtility.RevealInFinder(path);
        }
    }

    internal static class SkillV2ConfigUtil
    {
        /// <summary>Copy SO to pure data so JsonUtility can serialize it.</summary>
        public static SkillConfigData CloneToData(SkillConfigSO so)
        {
            // Because SO's nested defs are already [Serializable], we can deep-clone via JSON roundtrip.
            // (fast enough for editor usage; avoids writing a manual copier)
            var json = JsonUtility.ToJson(so, prettyPrint: false);
            return SkillV2Json.LoadFromJsonText(json);
        }
    }
}
#endif
