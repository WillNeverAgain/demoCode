using System;
using System.Collections.Generic;
using System.Linq;
using Range;
using Range.Interface;
using Tile.Base;
using UnityEngine;
namespace Tile.Middleware.CellRangeMiddleware
{
    public class DefaultCellRangeMapMiddleware : ICellRangeMapMiddleware
    {

        private IGameMapObjectContext _model;
        private IMapContextGetter _context;

        private IGameMapPositionContext _mapPositionContext;
        public IMapMiddleware Connect(IMapContextGetter blackboard)
        {
            _context = blackboard;
            _model = blackboard.GetContext<IGameMapObjectContext>();
            _mapPositionContext = blackboard.GetContext<IGameMapPositionContext>();
            return this;
        }
        public IMapMiddleware CloneMiddleware()
        {
             var temp=new DefaultCellRangeMapMiddleware();
             temp.Connect(_context);
             return temp;
        }
        public IReadOnlyList<ITagObject> FindObjectWithTagInRange(Vector3 findPosition, Func<IList<string>,bool> tagCharger, IRangeArrayInfo rangeArrayInfo)
        {
            
            var  pos= _mapPositionContext.WorldPosToCell(findPosition);
            List<ITagObject> objects = new List<ITagObject>();
            for (int i = 0; i < rangeArrayInfo.MaxLayer; i++)
            {
                for (int j = 0; j < rangeArrayInfo.MaxGroup; j++)
                {
                    //获取不存在阻塞条件
                    var ls= rangeArrayInfo.GetRanges(i, j);
                    foreach (var posOffset in ls)
                    {
                        var objTemps= _model.GetCellAndObjects(pos + posOffset);
                        foreach (var objTemp in objTemps)
                        {
                            if (objTemps.Any(a => tagCharger.Invoke(a.Tags)))
                            {
                                objects.Add(objTemp);
                            }
                        }
                    }
                }
            }
            return objects;
        }
        public IReadOnlyList<IGameTileCell> GetCellWithTagInRange(Vector3 findPosition, Func<IList<string>, bool> tagCharger, IRangeArrayInfo rangeArrayInfo)
        {
            List<IGameTileCell> cellsList = new List<IGameTileCell>();
            var  pos= _mapPositionContext.WorldPosToCell(findPosition);
            List<ITagObject> objects = new List<ITagObject>();
            for (int i = 0; i < rangeArrayInfo.MaxLayer; i++)
            {
                for (int j = 0; j < rangeArrayInfo.MaxGroup; j++)
                {
                    //获取不存在阻塞条件
                    var ls= rangeArrayInfo.GetRanges(i, j);
                    foreach (var posOffset in ls)
                    {
                        var objTemps= _model.GetCell(pos + posOffset);
                        if (tagCharger.Invoke(objTemps.Tags))
                        {
                                objects.Add(objTemps);
                        }
                    }
                }
            }
            return cellsList;
        }
        public IReadOnlyList<IGameTileCell> GetCellWithTagInRange(Vector3 findPosition, Func<IList<string>, bool> tagCharger, IEnumerable<Vector2Int> rangeArray)
        {
            return GetCellWithTagInRange(findPosition,tagCharger,new DefaultRangeArrayInfo(rangeArray));
        }
        public IReadOnlyList<ITagObject> FindObjectWithTagInRange(Vector3 findPosition, string tag, IRangeArrayInfo rangeArrayInfo)
        {
           return FindObjectWithTagInRange(findPosition,t=>t.Contains(tag),rangeArrayInfo);
        }
        public IReadOnlyList<ITagObject> FindObjectWithTagInRange(Vector3 findPosition, Func<IList<string>,bool> tagCharger, IEnumerable<Vector2Int> rangeArray)
        {
            return FindObjectWithTagInRange(findPosition,tagCharger,new DefaultRangeArrayInfo(rangeArray));
        }
    }
}