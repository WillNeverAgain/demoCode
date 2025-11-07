using System.Collections.Generic;
using System.Linq;
using ODG.Process.Dungeon.UI;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
namespace ODG.Process.Dungeon
{
    public class DungeonTargetCollection : MonoBehaviour
    {
        
        [SerializeField]
        [InlineButton("LoadTargets","获取目标")]
        [CustomValueDrawer("DungeonObjListDrawer")]
        private List<DungeonTarget> _targets = new List<DungeonTarget>();
        public IEnumerable<string> GetTargetDesc()
        {
            return _targets.Select(t => t.GetDescription());
        }

        private void LoadTargets()
        {
            _targets.Clear();
           var res= gameObject.GetComponents<DungeonTarget>();
           _targets.AddRange(res);
        }
        
        
        private DungeonTarget DungeonObjListDrawer(DungeonTarget value, GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();
    
            // 检查对象是否丢失
            bool isMissing = value == null;
            if (isMissing)
            {
                GUI.color = Color.red;
            }
    
            // 中间显示物体引用（只读）
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField(value, typeof(DungeonObj), true, GUILayout.MinWidth(100));
            EditorGUI.EndDisabledGroup();
    
            // 右边显示名字
            string nameText = isMissing ? "Missing" : (string.IsNullOrEmpty(value.TargetDescription) ? "No Name" : value.TargetDescription);
            EditorGUILayout.LabelField($"Des :{nameText}", GUILayout.ExpandWidth(true));
    
            // 恢复颜色
            if (isMissing)
            {
                GUI.color = Color.white;
            }
    
            EditorGUILayout.EndHorizontal();
    
            return value; // 返回原始值，因为是只读的
        }
    }
}