using System;
using UnityEditor;
using UnityEngine;
namespace Tile.MapEditor.ScenceEditor
{
    /// <summary>
    /// 场景地图信息编辑用的标记
    /// </summary>
    public class SceneDataTileMark : MonoBehaviour
    {
        private GameObject manager;
        private void Start()
        {
            manager= GameObject.Find("Manager");

           manager.GetComponent<SceneDataWriter>().CreateCell(this);
        }
    }
}