using System;
using Cysharp.Threading.Tasks;
using ODG.UI.Animation;
using UnityEngine;
namespace ODG.UI
{
    public class BlackScream : MonoBehaviour
    {
        public static BlackScream Instance {
            get {
                return GameManager.BlackScream;
            }
        }
        
        [SerializeField] private FadeGroups _blackScreamCanvasGroup;

        public async UniTask FadeBlackScream(Func<UniTask> callback)
        {
            await _blackScreamCanvasGroup.Show();
            await callback.Invoke();
            await _blackScreamCanvasGroup.Hide();
        }
    }
}