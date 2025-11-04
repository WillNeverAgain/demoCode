using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ODG.UI.Animation
{
    /// <summary>
    /// 透明度控制组件
    /// 
    /// </summary>
    public abstract class FadeElement : MonoBehaviour
    {
        protected CancellationToken ActiveTaskToken {
            get {
                if (!usingToken)
                {
                    return CancellationToken.None;
                }
                else if (_currentAnimationCTS == null)
                {
                    return CancellationToken.None;
                }
                return _currentAnimationCTS.Token;
            }
        }
        
        public void SkipHide()
        {
            OnSkipHide();
        }
        protected abstract void OnSkipHide();
        public void SkipShow()
        {
            OnSkipShow();
        }
        protected abstract void OnSkipShow();
        public async UniTask HideWithUniTaskReturn(CancellationToken token)
        {
            IsFading = true;
            if(token!=null)
                token.ThrowIfCancellationRequested();
    
            // 取消并释放之前的操作
            _currentAnimationCTS?.Cancel();
            _currentAnimationCTS?.Dispose();
            _currentAnimationCTS = null;
    
            // 创建新的TokenSource，并链接外部token
            _currentAnimationCTS = CancellationTokenSource.CreateLinkedTokenSource(token);
    
            try
            {
                isShown = false;
                await UniTask.Delay(TimeSpan.FromSeconds(hideDelay), DelayType.UnscaledDeltaTime, cancellationToken: _currentAnimationCTS.Token);
                await OnHideWithUniTaskReturn(_currentAnimationCTS.Token);
                IsFading = false;
            }
            catch (OperationCanceledException)
            {
                #if UNITY_EDITOR
                Debug.Log("动画中断");
                #endif
                // 取消异常处理
                throw;
            }
        }
        protected abstract UniTask OnHideWithUniTaskReturn(CancellationToken token);
        
        public async UniTask ShowWithUniTaskReturn(CancellationToken token)
        {
            IsFading = true;
            if(token!=null)
                token.ThrowIfCancellationRequested();
    
            // 取消并释放之前的操作
            _currentAnimationCTS?.Cancel();
            _currentAnimationCTS?.Dispose();
            _currentAnimationCTS = null;
    
            // 创建新的TokenSource，并链接外部token
            _currentAnimationCTS = CancellationTokenSource.CreateLinkedTokenSource(token);
    
            try
            {
                isShown = false;
                await UniTask.Delay(TimeSpan.FromSeconds(showDelay), DelayType.UnscaledDeltaTime, cancellationToken: _currentAnimationCTS.Token);
                await OnShowWithUniTaskReturn(_currentAnimationCTS.Token);
                IsFading = false;
            }
            catch (OperationCanceledException)
            {
                #if UNITY_EDITOR
                Debug.Log("动画中断");
                #endif
                // 取消异常处理
                throw;
            }
        }
        protected abstract UniTask OnShowWithUniTaskReturn(CancellationToken token);
        
        
        
        [Header("基础设定")]
        /// <summary>
        /// 使用token (中断之前的task)
        /// </summary>
        [SerializeField]
        private bool usingToken = true;
        /// <summary>
        /// 是否管理物体的开关
        /// </summary>
        [SerializeField]
        private bool manageGameObjectActive;
        
        /// <summary>
        /// 延时
        /// </summary>
        [SerializeField]
        private float showDelay;
        [SerializeField]  
        private float hideDelay;

        /// <summary>
        /// 目前状态的标识符
        /// 播放播放前修改
        /// </summary>
        private bool isShown;
        
        public bool IsFading { get; private set; }
        
        /// <summary>
        /// 用于取消动画
        /// </summary>
        private CancellationTokenSource _currentAnimationCTS;
    }
}
