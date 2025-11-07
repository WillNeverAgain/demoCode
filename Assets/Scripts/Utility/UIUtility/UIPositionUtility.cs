using UnityEngine;
namespace ODG.Utility.UIUtility
{
    /// <summary>
    /// UI的一些位置计算
    /// </summary>
    public static class UIPositionUtility
    {
        public static Vector3 ScreenToWorldPoint(Camera targetCamera,Vector2 screenPoint,int zLayer = 0)
        {
            var pos= targetCamera.ScreenToWorldPoint(screenPoint);
            pos.z = zLayer;
            return pos;
        }
    }
}