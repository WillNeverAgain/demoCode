using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniTest;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UniTestEditor : EditorWindow
{
    [SerializeField] private VisualTreeAsset m_VisualTreeAsset;
    
    // UI元素引用
    private DropdownField typeDropdown;
    private ScrollView buttonScrollView;
    private Dictionary<string, Type> providerTypes = new Dictionary<string, Type>();

    [MenuItem("Utility/UniTaskWindow")]
    public static void ShowWindow()
    {
        var wnd = GetWindow<UniTestEditor>();
        wnd.titleContent = new GUIContent("Method Invoker");
        wnd.minSize = new Vector2(300, 400);
    }

    public void CreateGUI()
    {
        // 加载UXML模板
        VisualElement root = rootVisualElement;
        m_VisualTreeAsset.CloneTree(root);

        // 获取UI元素引用
        typeDropdown = root.Q<DropdownField>("TypeChoice");
        buttonScrollView = root.Q<ScrollView>("ButtonScroll");

        // 注册选择变更事件
        typeDropdown.RegisterValueChangedCallback(OnTypeSelected);

        // 初始化下拉菜单
        InitializeTypeDropdown();
        
    }

    private void InitializeTypeDropdown()
    {
        providerTypes.Clear();
        typeDropdown.choices.Clear();

        // 获取所有程序集
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            // 过滤系统程序集提升性能
            if (assembly.FullName.StartsWith("Unity") || 
                assembly.FullName.StartsWith("System"))
                continue;
            
            try
            {
                // 获取所有带有特性标记的类
                foreach (var type in assembly.GetTypes()
                    .Where(t => t.IsDefined(typeof(UniTestAttribute))))
                {
                    var displayName = $"{type.Name} [{assembly.GetName().Name}]";
                    providerTypes[displayName] = type;
                    typeDropdown.choices.Add(displayName);
                }
            }
            catch (ReflectionTypeLoadException)
            {
                // 跳过无法加载的类型
            }
        }

        if (typeDropdown.choices.Count > 0)
        {
            typeDropdown.value = typeDropdown.choices[0];
        }
    }

    private void OnTypeSelected(ChangeEvent<string> changeEvent)
    {
        buttonScrollView.Clear();
        
        if (!providerTypes.TryGetValue(changeEvent.newValue, out var selectedType))
            return;

        // 获取所有公共静态方法（过滤编译器生成的方法）
        var methods = selectedType.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => !m.IsSpecialName && m.DeclaringType == selectedType);

        foreach (var method in methods)
        {
            // 创建调用按钮
            var button = new Button(() => InvokeMethod(method))
            {
                text = method.Name,
                style = 
                {
                    height = 25,
                    marginTop = 5,
                    marginBottom = 5,
                    unityTextAlign = TextAnchor.MiddleLeft
                }
            };
            
            // 添加工具提示显示完整签名
            button.tooltip = $"{method.ReturnType.Name} {method.Name}()";
            
            buttonScrollView.Add(button);
        }
    }

    private void InvokeMethod(MethodInfo method)
    {
        try
        {
            method.Invoke(null, null);
            Debug.Log($"UniTest方法执行成功: {method.DeclaringType.Name}.{method.Name}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"UniTest方法执行失败: {method.Name}\n{ex.InnerException?.Message ?? ex.Message}");
        }
    }
}