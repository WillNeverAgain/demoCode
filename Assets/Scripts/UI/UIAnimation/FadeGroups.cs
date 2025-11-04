using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
namespace ODG.UI.Animation
{
    public class FadeGroups : MonoBehaviour
    {

        
        public event Action<FadeGroups> OnFadeComplete;
        public event Action<FadeGroups> OnShowComplete;
        public event Action<FadeGroups> OnHideComplete;

        public bool IsShowingProgress=>isShowingProgress;
        public bool IsHidingProgress=>isHidingProgress;
        
        public bool IsShow => isShow;
        public bool IsHide => !isShow;

        
        private void OnEnable()
        {
            if (_showOnEnable) Show();
        }
        public async UniTask Show()
        {
            if (manageGameObjectActive)
            {
                gameObject.SetActive(true);
            }
            isShowingProgress = true;

            // 取消并释放之前的操作
            _currentAnimationCTS?.Cancel();
            _currentAnimationCTS?.Dispose();
            _currentAnimationCTS = null;
    
            // 创建新的TokenSource，并链接外部token
            _currentAnimationCTS = new CancellationTokenSource();
            var res= _fadeElements.Select(te => te.ShowWithUniTaskReturn(_currentAnimationCTS.Token));
            await UniTask.WhenAll(res);
            isShow = true;
            isShowingProgress = false;
        }

        public void SkipShow()
        {
            foreach (FadeElement fadeElement in _fadeElements)
            {
                fadeElement.SkipShow();
            }
        }

        public async UniTask Hide()
        {
            isHidingProgress = true;

            // 取消并释放之前的操作
            _currentAnimationCTS?.Cancel();
            _currentAnimationCTS?.Dispose();
            _currentAnimationCTS = null;
    
            // 创建新的TokenSource，并链接外部token
            _currentAnimationCTS = new CancellationTokenSource();
            var res= _fadeElements.Select(te => te.HideWithUniTaskReturn(_currentAnimationCTS.Token));
            await UniTask.WhenAll(res);
            isShow = false;
            isHidingProgress = false;
            if (manageGameObjectActive)
            {
                gameObject.SetActive(false);
            }
        }
        public void SkipHide()
        {
            foreach (FadeElement fadeElement in _fadeElements)
            {
                fadeElement.SkipHide();
            }
        }
        
        [SerializeField] 
        private List<FadeElement> _fadeElements = new List<FadeElement>();
        
        [SerializeField]
        private bool _skipHideOnStart = true;

        [SerializeField]
        private bool _showOnEnable;

        [SerializeField]
        private bool _skipHideBeforeShow = true;
        
        [SerializeField]
        public bool manageGameObjectActive;
        
        private bool isShow = false;

        private CancellationTokenSource _currentAnimationCTS;
        
        
        private bool isShowingProgress;
        private bool isHidingProgress;

    }
}