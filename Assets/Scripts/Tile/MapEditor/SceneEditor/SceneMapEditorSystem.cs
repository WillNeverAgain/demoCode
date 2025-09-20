using System;
using Tile.Context;
using UnityEngine;
namespace Tile.MapEditor.ScenceEditor
{
    /// <summary>
    /// 管理编辑器内容的系统
    /// </summary>
    public class SceneMapEditorSystem : MonoBehaviour
    {
        public Grid grid;
        public UnityGridTilePositionContext ctx;
        private void Start()
        {
        }
        private void Awake()
        {           
            ctx = new UnityGridTilePositionContext(grid);
            ctx.Initialize(Vector3.zero);
        }
    }
}