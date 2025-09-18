using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tile.SO;
using UnityEngine;

public class MapEditorWindow : EditorWindow
{
    private const string UxmlPath = "Assets/EditorInterface/MapEditor/MapEditor.uxml";
    private const string UssPath = "Assets/EditorInterface/MapEditor/MapEditor.uss";
    
    private VisualElement root;
    private TreeView fileTree;
    private VisualElement editorMain;
    private VisualElement inspectorArea;
    private Button addButton;
    
    private MapData_SO currentMap;
    private List<MapData_SO> allMaps = new List<MapData_SO>();

    [MenuItem("Window/Map Table EditorInterface")]
    public static void ShowWindow()
    {
        GetWindow<MapEditorWindow>("Map Table EditorInterface");
    }

    private void OnEnable()
    {
        // 加载UXML和USS
        root = rootVisualElement;
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
        visualTree.CloneTree(root);
        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(UssPath);
        root.styleSheets.Add(styleSheet);

        // 获取UI元素引用
        fileTree = root.Q<TreeView>("FileTree");
        editorMain = root.Q<VisualElement>("EidtorMain");
        inspectorArea = root.Q<VisualElement>("Inspector");
        addButton = root.Q<Button>("Add");

        // 初始化文件树
        InitializeFileTree();
        
        // 注册事件
        addButton.clicked += CreateNewMap;
        
        // 设置TreeView的回调
        fileTree.makeItem = MakeTreeItem;
        fileTree.bindItem = BindTreeItem;
        fileTree.selectionChanged += OnMapSelected;
        
        // 注册单元格选择事件
        EditorEvents.OnCellSelected += ShowCellInspector;
    }

    private void OnDisable()
    {
        // 清理事件订阅
        EditorEvents.OnCellSelected -= ShowCellInspector;
    }

    private VisualElement MakeTreeItem()
    {
        return new Label();
    }

    private void BindTreeItem(VisualElement element, int index)
    {
        var label = element as Label;
        if (label != null && index >= 0 && index < allMaps.Count)
        {
            label.text = allMaps[index].mapName;
        }
    }

    private void InitializeFileTree()
    {
        // 加载所有MapData_SO
        allMaps = AssetDatabase.FindAssets("t:MapData_SO")
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .Select(path => AssetDatabase.LoadAssetAtPath<MapData_SO>(path))
            .ToList();

        // 正确设置TreeView的数据源
        fileTree.SetRootItems(allMaps.Select(map => 
            new TreeViewItemData<MapData_SO>(map.GetInstanceID(), map)).ToList());
        
        fileTree.Rebuild();
        
        // 默认选择第一个地图
        if (allMaps.Count > 0)
        {
            fileTree.SetSelection(0);
            LoadMap(allMaps[0]);
        }
    }

    private void CreateNewMap()
    {
        var newMap = CreateInstance<MapData_SO>();
        newMap.mapName = $"NewMap_{allMaps.Count + 1}";
        
        string path = EditorUtility.SaveFilePanelInProject(
            "Save New Map",
            newMap.mapName,
            "asset",
            "Select save location");
        
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newMap, path);
            AssetDatabase.SaveAssets();
            allMaps.Add(newMap);
            
            // 更新TreeView
            fileTree.SetRootItems(allMaps.Select(map => 
                new TreeViewItemData<MapData_SO>(map.GetInstanceID(), map)).ToList());
            
            fileTree.Rebuild();
            fileTree.SetSelection(allMaps.Count - 1);
        }
    }

    private void OnMapSelected(IEnumerable<object> selectedItems)
    {
        if (selectedItems.Any())
        {
            int index = fileTree.selectedIndex;
            if (index >= 0 && index < allMaps.Count)
            {
                LoadMap(allMaps[index]);
            }
        }
    }

    private void LoadMap(MapData_SO map)
    {
        currentMap = map;
        editorMain.Clear();
        inspectorArea.Clear();
        
        // 创建表格标题
        var title = new Label($"Editing: {map.mapName}");
        title.AddToClassList("map-title");
        editorMain.Add(title);
        
        // 创建控制按钮
        var controls = new VisualElement { style = { flexDirection = FlexDirection.Row } };
        var addRowBtn = new Button(AddNewRow) { text = "Add Row" };
        var addColBtn = new Button(AddNewColumn) { text = "Add Column" };
        controls.Add(addRowBtn);
        controls.Add(addColBtn);
        editorMain.Add(controls);
        
        // 创建表格容器
        var tableContainer = new ScrollView();
        editorMain.Add(tableContainer);
        
        // 渲染表格
        RenderTable(tableContainer);
    }

    private void RenderTable(VisualElement container)
    {
        if (currentMap == null || currentMap.mapData == null) return;
        
        container.Clear();
        
        for (int row = 0; row < currentMap.mapData.Count; row++)
        {
            var rowContainer = new VisualElement { style = { flexDirection = FlexDirection.Row } };
            
            for (int col = 0; col < currentMap.mapData[row].Count; col++)
            {
                var cell = currentMap.mapData[row][col];
                var cellBtn = new Button(() => {
                    EditorEvents.RaiseCellSelected(cell);
                })
                {
                    text = $"{row},{col}",
                    style = { width = 80, height = 30 }
                };
                
                rowContainer.Add(cellBtn);
            }
            
            container.Add(rowContainer);
        }
    }

    private void AddNewRow()
    {
        if (currentMap == null) return;
        
        int colCount = currentMap.mapData.Count > 0 ? currentMap.mapData[0].Count : 1;
        var newRow = new List<MapCellData_SO>();
        
        for (int col = 0; col < colCount; col++)
        {
            var cell = CreateCellSO(currentMap.mapData.Count, col);
            newRow.Add(cell);
        }
        
        currentMap.mapData.Add(newRow);
        EditorUtility.SetDirty(currentMap);
        AssetDatabase.SaveAssets();
        
        RenderTable(editorMain.Q<ScrollView>());
    }

    private void AddNewColumn()
    {
        if (currentMap == null) return;
        
        for (int row = 0; row < currentMap.mapData.Count; row++)
        {
            var cell = CreateCellSO(row, currentMap.mapData[row].Count);
            currentMap.mapData[row].Add(cell);
        }
        
        EditorUtility.SetDirty(currentMap);
        AssetDatabase.SaveAssets();
        
        RenderTable(editorMain.Q<ScrollView>());
    }

    private MapCellData_SO CreateCellSO(int row, int col)
    {
        string folderPath = $"Assets/Data/{currentMap.mapName}/";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            AssetDatabase.Refresh();
        }

        string path = $"{folderPath}Cell_{row}_{col}.asset";
        var cell = AssetDatabase.LoadAssetAtPath<MapCellData_SO>(path);
        
        if (cell == null)
        {
            cell = CreateInstance<MapCellData_SO>();
            cell.row = row;
            cell.col = col;
            cell.cellName = $"Cell_{row}_{col}";
            AssetDatabase.CreateAsset(cell, path);
            AssetDatabase.SaveAssets();
        }
        
        return cell;
    }

    private void ShowCellInspector(MapCellData_SO cell) 
    {
        inspectorArea.Clear();
        if (cell == null) return;

        // 使用自定义UIElements Inspector
        var inspectorUI = cell.CreateInspectorUI();
        inspectorArea.Add(inspectorUI);
    }
}

// ============== 事件中心 ==============
public static class EditorEvents
{
    public static event System.Action<MapCellData_SO> OnCellSelected;

    public static void RaiseCellSelected(MapCellData_SO cell)
    {
        OnCellSelected?.Invoke(cell);
    }
}

// ============== 自定义Inspector ==============
[CustomEditor(typeof(MapCellData_SO))]
public class MapCellInspector : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        var cell = target as MapCellData_SO;
        
        // 单元格基本信息
        var nameField = new TextField("Cell Name") {
            value = cell.cellName
        };
        nameField.RegisterValueChangedCallback(e => {
            cell.cellName = e.newValue;
            EditorUtility.SetDirty(cell);
        });
        
        var rowField = new IntegerField("Row") {
            value = cell.row,
            isReadOnly = true
        };
        
        var colField = new IntegerField("Column") {
            value = cell.col,
            isReadOnly = true
        };
        
        root.Add(nameField);
        root.Add(rowField);
        root.Add(colField);
        
        // 添加其他自定义字段
        var properties = serializedObject.FindProperty("dataArgs");
        if (properties != null)
        {
            var argsContainer = new Foldout {
                text = "Cell Arguments",
                value = false
            };
            
            var listView = new ListView {
                itemsSource = cell.dataArgs,
                makeItem = () => new TextField(),
                bindItem = (element, index) => {
                    var field = element as TextField;
                    field.value = cell.dataArgs[index];
                    field.RegisterValueChangedCallback(e => {
                        cell.dataArgs[index] = e.newValue;
                        EditorUtility.SetDirty(cell);
                    });
                }
            };
            
            argsContainer.Add(listView);
            root.Add(argsContainer);
        }
        
        return root;
    }
}

// ============== 数据接口 ==============
public interface ICellInspectorProvider
{
    VisualElement CreateInspectorUI();
}

[CreateAssetMenu(fileName = "MapCellData_SO", menuName = "SO/MapCellData_SO")]
public class MapCellData_SO : ScriptableObject, ICellInspectorProvider
{
    public int row;
    public int col;
    public string cellName;
    public List<string> dataArgs = new List<string>();
    
    public VisualElement CreateInspectorUI()
    {
        // 使用自定义Editor创建UI
        var editor = Editor.CreateEditor(this);
        return editor.CreateInspectorGUI();
    }
}

[CreateAssetMenu(fileName = "MapData_SO", menuName = "SO/MapData_SO")]
public class MapData_SO : ScriptableObject
{
    public string mapName;
    public List<List<MapCellData_SO>> mapData = new List<List<MapCellData_SO>>();
}