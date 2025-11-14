
using System.Collections;
using System.Collections.Generic;
using ODG.Utility.UIUtility;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
namespace ODG.Process.Dungeon.UI
{
    /// <summary>
    /// 章节物体
    /// </summary>
    public class DungeonArrayObj : MonoBehaviour
    {
        
        [InfoBox("关卡的父对象，无需配置")]
        [SerializeField]private Transform dungeonContainer;
        
        /// <summary>
        /// 关卡信息
        /// </summary>
        [SerializeField]
        [InlineButton("AutoGetDungeonObjList","自动获取子物体")]
        [CustomValueDrawer("DungeonObjListDrawer")]
        private List<DungeonObj> _dungeonObjList = new List<DungeonObj>();

        public void AutoGetDungeonObjList()
        {
            var duList= dungeonContainer.GetComponentsInChildren<DungeonObj>();
            _dungeonObjList.Clear();
            foreach (var re in duList)
            {
                _dungeonObjList.Add(re);
            }
        }
        // 可选：添加更详细的工具提示
        private DungeonObj DungeonObjListDrawer(DungeonObj value, GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();
    

            // 检查对象是否丢失
            bool isMissing = value == null;
            if (isMissing)
            {
                GUI.color = Color.red;
            }
    
            // 左边显示ID
            string idText = isMissing ? "Missing" : (string.IsNullOrEmpty(value.ID) ? "No ID" : value.ID);
            EditorGUILayout.LabelField($"ID: {idText}", GUILayout.Width(80));
    
            // 中间显示物体引用（只读）
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField(value, typeof(DungeonObj), true, GUILayout.MinWidth(100));
            EditorGUI.EndDisabledGroup();
    
            // 右边显示名字
            string nameText = isMissing ? "Missing" : (string.IsNullOrEmpty(value.DungeonName) ? "No Name" : value.DungeonName);
            EditorGUILayout.LabelField($"Name :{nameText}", GUILayout.ExpandWidth(true));
    
            // 恢复颜色
            if (isMissing)
            {
                GUI.color = Color.white;
            }
    
            EditorGUILayout.EndHorizontal();
    
            
            if (EditorGUI.EndChangeCheck())
            {
                // 标记对象为脏以便保存
                EditorUtility.SetDirty(this);
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
            }
            return value; // 返回原始值，因为是只读的
        }
    }
}
