using System;
using Tile.Base;
using UnityEngine;
namespace Tile.Context
{
    
    
    public static class RhombusMapCore
    {
        // 世界位置 → 逻辑位置（精确边界计算）
        public static Vector2Int WorldToLogicalCore(
            Vector3 worldPos, 
            Vector3 origin, 
            float longAxis, 
            float shortAxis)
        {
            // 计算相对于网格原点的位置
            float relX = worldPos.x - origin.x;
            float relY = worldPos.y - origin.y;
        
            // 计算在网格坐标系中的位置
            float gridX = relX / longAxis;
            float gridY = relY / shortAxis;
        
            // 使用基于网格边界的方法确定逻辑位置
            int i = CalculateGridIndex(gridX);
            int j = CalculateGridIndex(gridY);
        
            return new Vector2Int(i, j);
        }
    
        // 精确计算网格索引（处理边界情况）
        private static int CalculateGridIndex(float position)
        {
            // 获取整数和小数部分
            float integerPart = (float)Math.Floor(position);
            float fractionalPart = position - integerPart;
        
            // 边界处理：当接近0.5时，根据方向确定归属
            if (Math.Abs(fractionalPart - 0.5f) < 0.001f)
            {
                // 根据移动方向确定归属（假设正方向移动）
                return position > 0 ? (int)Math.Ceiling(position) : (int)Math.Floor(position);
            }
        
            // 常规情况：四舍五入
            return (int)Math.Round(position);
        }

        // 逻辑位置 → 世界位置（保持不变）
        public static Vector3 LogicalToWorldCore(
            Vector2Int logicalPos, 
            Vector3 origin, 
            float longAxis, 
            float shortAxis)
        {
            float x = origin.x + logicalPos.x * longAxis;
            float y = origin.y + logicalPos.y * shortAxis;
            return new Vector3(x, y, origin.z);
        }
    }
    /// <summary>
    /// 地图的逻辑位置应该符合下面公式
    ///                
    ///     0,1      2,2
    ///            1,1      2,1
    ///     0,0       1,0       2,0 
    ///            1,-1
    ///     0,-1     
    ///
    ///     旋转矩阵 Cell To  Logical  (矩阵加取下限)
    ///     x           y           z           w
    ///     1           1           0           _logicalOffset.x
    ///     -0.5       0.5        0           _logicalOffset.y
    ///     0           0           1           0
    ///     0           0           0           1
    ///
    ///     旋转矩阵 Logical To  Cell   (矩阵加取下限)  再除 根号2
    ///     x           y           z           w
    ///     one_divide_root_tow      -one_divide_root_tow         0        -_logicalOffset.x
    ///     one_divide_root_tow       one_divide_root_tow          0       -_logicalOffset.y
    ///     0           0           1           0
    ///     0           0           0           1
    /// </summary>
    public class RhombusGridTilePositionContext: IGameMapPositionContext
    {
           private Vector3 _positionOffset=Vector3.zero;

           private Vector2Int _logicalPosition = new Vector2Int(0, 0);
           private float _longAxis=2;
           private float _shortAxis=1;

        /// <param name="offset">偏移量</param>
        public void Initialize(Vector3 wordOffset)
        {
            //避免重复初始化
            if(_positionOffset.Equals(wordOffset))return;
            _positionOffset = wordOffset;
            _logicalPosition = WorldPosToLogic(_positionOffset);
        }
        public Vector3 LogicPosToWorld(int x, int y)
        {
            return RhombusMapCore.LogicalToWorldCore(new Vector2Int(x,y), _positionOffset,_longAxis,_shortAxis);
        }
        public Vector2Int WorldPosToLogic(Vector3 pos)
        { 
           return RhombusMapCore.WorldToLogicalCore(pos, Vector2.left,_longAxis,_shortAxis);
        }
        public Vector2Int LogicPosToCell(int x, int y)
        {
            return new Vector2Int(x,y)-_logicalPosition;
        }
        /// <summary>
        /// 默认在中间
        /// </summary>
        public Vector2Int CellPosToLogic(int x, int y)
        {
            return  new Vector2Int(x,y)+_logicalPosition;
        }
        public Vector3 CellPosToWorld(Vector2Int pos)
        {
            return LogicPosToWorld(CellPosToLogic(pos));
        }
        public Vector2Int WorldPosToCell(Vector3 worldPos)
        {
            return LogicPosToCell(WorldPosToLogic(worldPos));
        }
        public Vector3 CellPosToWorld(int x, int y)
        {
           return CellPosToWorld(new Vector2Int(x,y));
        }

        public Vector2Int LogicPosToCell(Vector2Int position)
        {
          return LogicPosToCell(position.x,position.y);
        }
        
        public Vector2Int CellPosToLogic(Vector2Int pos)
        {
            return CellPosToLogic(pos.x, pos.y);
        }
        
        public Vector3 LogicPosToWorld(Vector2Int pos)
        {
            return LogicPosToWorld(pos.x, pos.y);
        }
    }
}