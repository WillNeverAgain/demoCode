using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace ODG.Utility.UIUtility
{
    /// <summary>
    /// 方便后面换加载形式的接口
    /// </summary>
    public interface IResourceLoadUtility
    {
        public T Load<T>(string path) where T : UnityEngine.Object;
    }
    public class ResourceLoadUtility: MonoBehaviour , IResourceLoadUtility
    {
        [SerializeField]
        private string _resourceBasePath;
        
        public T Load<T>(string path) where T : Object
        {
            var res= Resources.Load<T>(Path.Combine(_resourceBasePath, path));
            return res;
        }
    }
}