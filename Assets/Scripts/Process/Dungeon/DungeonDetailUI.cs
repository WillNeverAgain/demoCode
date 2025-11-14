using System.Text;
using ODG.UI.Animation;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace  ODG.Process.Dungeon.UI
{
    public class DungeonDetailUI : MonoBehaviour , IPointerClickHandler ,IPointerExitHandler
    {   
        [FoldoutGroup("Bind")]
        [SerializeField]
        private TextMeshProUGUI _titleText;
        
        [FoldoutGroup("Bind")]
        [SerializeField]
        private TextMeshProUGUI _descriptionText;
        
        [FoldoutGroup("Bind")]
        [SerializeField]
        private TextMeshProUGUI _targetText;
        
        [FoldoutGroup("Bind")]
        [SerializeField]
        private Image _dungeonImage;

        [FoldoutGroup("Bind")]
        [SerializeField]
        private FadeGroups _fadeGroups;

        [FoldoutGroup("Bind")]
        [SerializeField]
        private LoadSceneButton _loadSceneButton;
        
        [ReadOnly]
        [SerializeField]
        private DungeonObj _targetObj;
        public void SetUp(DungeonObj dungeonObj)
        {
            _targetObj = dungeonObj;
        }
        public void Refresh()
        {
            _titleText.text= _targetObj.DungeonName;
            StringBuilder sb = new StringBuilder();
            foreach (var des in _targetObj.TargetDescription)
            {
                sb.AppendLine(des);
            }
            _targetText.text = sb.ToString();
            _descriptionText.text = _targetObj.DungeonDescription;
            _dungeonImage.sprite = _targetObj.DungeonPreviewSprite;
            _loadSceneButton.SetScene(_targetObj.ScenePath);
        }
        public void Show(Canvas targetCanvas)
        {
            _fadeGroups.Show();
        }
        public void Hide()
        {
            _fadeGroups.Hide();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
}

