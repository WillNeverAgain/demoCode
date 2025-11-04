using ODG.UI;
using UnityEngine;
namespace ODG
{
    /// <summary>
    /// 全局单例存放
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {
            get {
                if (!Application.isPlaying)
                {
                    return null;
                }
                if (s_instance == null)
                {
                    s_instance = FindObjectOfType<GameManager>();
                    if (s_instance)
                    {
                        DontDestroyOnLoad(s_instance.gameObject);
                    }
                }
                if (s_instance == null)
                {
                    GameObject gameObject = Resources.Load<GameObject>("GameManager");
                    if (gameObject == null)
                    {
                        Debug.LogError("Resources中找不到GameManager的Prefab");
                    }
                    GameManager component = Instantiate<GameObject>(gameObject).GetComponent<GameManager>();
                    if (component == null)
                    {
                        Debug.LogError("GameManager的prefab上没有GameManager组件");
                        return null;
                    }
                    s_instance = component;
                    if (s_instance)
                    {
                        DontDestroyOnLoad(s_instance.gameObject);
                    }
                }
                return s_instance;
            }
        }

        public static BlackScream BlackScream {
            get {
                return GameManager.Instance._blackScream;
            }
        }
        
        private static GameManager s_instance;
        
        [SerializeField]
        private BlackScream _blackScream;
    }
}

