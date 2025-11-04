using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
namespace ODG.UI.Animation
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupFade : FadeElement
    {
        [SerializeField] 
        [Header("配置内容")]
        private CanvasGroup _canvasGroup;
        
        [SerializeField]
        [Header("消耗的时间")]
        private float duration;
        protected override void OnSkipHide()
        {
            _canvasGroup.alpha = 0;            
        }
        protected override void OnSkipShow()
        {
            _canvasGroup.alpha = 1;
        }
        protected override UniTask OnHideWithUniTaskReturn(CancellationToken token)
        {
            return _canvasGroup.DOFade(0, duration).Play().ToUniTask(cancellationToken: token);
        }
        protected override UniTask OnShowWithUniTaskReturn(CancellationToken token)
        {
            return _canvasGroup.DOFade(1, duration).Play().ToUniTask(cancellationToken: token);
        }
    }
}