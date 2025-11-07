using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using TMPro;
using UnityEngine.Serialization;

namespace ODG.Process.Dungeon.UI
{
    /// <summary>
    /// 游戏内实际的关卡信息对象
    /// 该对象处于UI层
    /// </summary>
    public class DungeonObj : MonoBehaviour
    {
        [FoldoutGroup("Binding")]
        [InfoBox("此处不需要更改，一些基础信息绑定")]
        [SerializeField]
        private TextMeshProUGUI levelText;
        [FoldoutGroup("Binding")]
        [SerializeField]
        private DungeonTargetCollection _targetCollection;
        
        [FoldoutGroup("Config", Order = 0)]
        [Header("=== 基础信息 ===")]
        [SerializeField, LabelText("关卡ID")]
        [Tooltip("关卡的唯一标识符")]
        [ValidateInput(nameof(ValidateButtonID), "关卡ID不能为空!", InfoMessageType.Error)]
        private string _buttonID;

        [FoldoutGroup("Config", Order = 0)]
        [SerializeField, LabelText("UI上的名字")]
        [OnValueChanged(nameof(OnUINameChanged))] 
        private string _uiName;
        
        [FoldoutGroup("Config")]
        [SerializeField, LabelText("关卡名称")]
        [Tooltip("显示给玩家的关卡名称")]
        [ValidateInput(nameof(ValidateDungeonName), "关卡名称为空，建议填写", InfoMessageType.Warning)]
        private string _name;

        [FoldoutGroup("Config")]
        [HorizontalGroup("Config/DisAndImage", Width = 0.85f)]
        [LabelText("关卡描述")]
        [SerializeField, TextArea(6, 10), HideLabel]
        [Tooltip("关卡的详细描述信息，每行将作为单独一段显示")]
        private string _description;
        
        [FoldoutGroup("Config")]
        [HorizontalGroup("Config/DisAndImage", Width = 0.15f)]
        [SerializeField, HideLabel]
        [PreviewField(FilterMode = FilterMode.Bilinear, Height = 80)]
        private Sprite _dungeonImageSprite;
        

        [FoldoutGroup("Config")]
        [SerializeField, LabelText("关联场景")]
        [InfoBox("场景路径为空，关卡将无法加载", InfoMessageType.Error, VisibleIf = nameof(IsScenePathEmpty))]
        private string _scenePath;

        [FoldoutGroup("Config")]
        /// <summary>
        /// 获取目标描述（只读显示）
        /// </summary>
        [ShowInInspector, LabelText("目标描述")]
        [ListDrawerSettings(NumberOfItemsPerPage = 5, ShowItemCount = true)]
        [PropertyOrder(10)]
        public List<string> TargetDescription
        {
            get
            {
                if (_targetCollection != null)
                    return _targetCollection.GetTargetDesc().ToList();
                return new List<string> { "未绑定目标集合" };
            }
        }
        
        /// <summary>
        /// 整体配置验证状态显示
        /// </summary>
        [FoldoutGroup("Config")]
        [InfoBox("配置存在错误，请检查红色标记的字段", InfoMessageType.Error, VisibleIf = nameof(HasConfigurationErrors))]
        [InfoBox("配置存在警告，请检查黄色标记的字段", InfoMessageType.Warning, VisibleIf = nameof(HasConfigurationWarningsOnly))]
        [InfoBox("配置验证通过", InfoMessageType.Info, VisibleIf = nameof(IsConfigurationValid))]
        [PropertyOrder(99)]
        private void ConfigurationStatus() { } // 空方法，仅用于显示InfoBox

        /// <summary>
        /// 快速验证按钮（可选保留）
        /// </summary>
        [FoldoutGroup("Config")]
        [Button("验证配置"), PropertyOrder(100)]
        private void ValidateConfiguration()
        {
            if (HasConfigurationErrors())
            {
                Debug.LogError("配置存在错误，请检查红色标记的字段!", this);
            }
            else if (HasConfigurationWarningsOnly())
            {
                Debug.LogWarning("配置存在警告，请检查黄色标记的字段", this);
            }
            else
            {
                Debug.Log("配置验证通过");
            }
        }

        [FoldoutGroup("Info", Order = 1)]
        [ReadOnly, SerializeField, LabelText("是否锁定")]
        private bool _hasLock;

        // 验证方法
        private bool ValidateButtonID() => !string.IsNullOrEmpty(_buttonID);
        private bool ValidateDungeonName() => !string.IsNullOrEmpty(_name);
        private bool ValidateTargetCollection() => _targetCollection != null;
        private bool IsScenePathEmpty() => string.IsNullOrEmpty(_scenePath);

        // 验证状态方法
        private bool HasConfigurationErrors() => string.IsNullOrEmpty(_buttonID) || string.IsNullOrEmpty(_scenePath);
        private bool HasConfigurationWarningsOnly() => !HasConfigurationErrors() && (string.IsNullOrEmpty(_name) || _targetCollection == null);
        private bool IsConfigurationValid() => !HasConfigurationErrors() && !HasConfigurationWarningsOnly();

        // 公开属性
        public string ID => _buttonID;
        public string DungeonName => _name;

        private void OnUINameChanged()
        {
            levelText.text = _uiName;
        }
    }
}