using System.Linq;
using UniTest;
namespace InsButton
{
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

[CustomEditor(typeof(MonoBehaviour), true)] // 覆盖所有MonoBehaviour
[CanEditMultipleObjects]
public class InspectorButtonEditor : Editor
{
    // 缓存反射结果（优化性能）
    private static Dictionary<Type, MethodInfo[]> _methodCache = new Dictionary<Type, MethodInfo[]>();
    
    public override void OnInspectorGUI()
    {
        // 1. 先绘制默认Inspector内容
        DrawDefaultInspector();
        
        // 2. 绘制自定义按钮区域（与核心逻辑分离）
        DrawInspectorButtons();
    }

    private void DrawInspectorButtons()
    {
        var methods = GetButtonMethods(target.GetType());
        if (methods.Length == 0) return;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("快捷操作", EditorStyles.boldLabel);
        
        foreach (var method in methods)
        {
            var attr = (InspectorButtonAttribute)Attribute.GetCustomAttribute(
                method, typeof(InspectorButtonAttribute));
                
            string buttonText = string.IsNullOrEmpty(attr.ButtonText) 
                ? ObjectNames.NicifyVariableName(method.Name) 
                : attr.ButtonText;
                
            if (GUILayout.Button(buttonText))
            {
                InvokeMethodSafe(method);
            }
        }
    }

    private MethodInfo[] GetButtonMethods(Type type)
    {
        if (_methodCache.TryGetValue(type, out var cached)) 
            return cached;
        
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => 
                m.GetParameters().Length == 0 &&
                Attribute.IsDefined(m, typeof(InspectorButtonAttribute))
            ).ToArray();
        
        _methodCache[type] = methods;
        return methods;
    }

    private void InvokeMethodSafe(MethodInfo method)
    {
        try
        {
            // 支持多对象同时操作
            foreach (var t in targets)
            {
                method.Invoke(t, null);
                
                // 标记场景脏数据（支持撤销操作）
                if (t is Component c && !Application.isPlaying)
                {
                    EditorUtility.SetDirty(c);
                    EditorSceneManager.MarkSceneDirty(c.gameObject.scene);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"按钮执行失败: {method.Name}\n{e.Message}");
        }
    }
}
#endif
}