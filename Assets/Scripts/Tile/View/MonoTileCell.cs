using System;
using Tile.Interface.Base;
using Tile.SO;
using UnityEngine;
namespace Tile.TillCell
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MonoTileCell : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public void SetInfo(in ICreateCellInfo createCellInfoSo)
        {
            spriteRenderer.sprite=createCellInfoSo.Sprite;
            spriteRenderer.sortingLayerID =spriteRenderer.sortingLayerID;
        }
    }
}