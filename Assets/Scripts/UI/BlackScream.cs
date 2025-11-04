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
        
        [SerializeField] private CanvasGroup _blackScreamCanvasGroup;
        
    }
}