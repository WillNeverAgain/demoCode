using Cysharp.Threading.Tasks;
using ODG.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadSceneButton : MonoBehaviour
{
    [SerializeField]
    private Button owner;
    
    private void Awake()
    {
        owner.onClick.AddListener(LoadScene);
    }


    [SerializeField,ReadOnly]
    private string _sceneName;
    
    public void SetScene(string sceneName)
    {
        this._sceneName = sceneName;
    }
    private void LoadScene()
    {
        BlackScream.Instance.FadeBlackScream(LoadSceneAsync);
    }
    private async UniTask LoadSceneAsync()
    {
        await SceneManager.LoadSceneAsync(_sceneName);
    }
}
