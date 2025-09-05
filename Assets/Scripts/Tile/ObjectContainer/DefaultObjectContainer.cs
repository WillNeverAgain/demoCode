using System.Collections.Generic;
using System.Linq;
using Tile.Base;
namespace Tile.ObjectContainer
{
    public class DefaultObjectContainer : IMapObjectContainer
    {
        public IList<ITagObject> GameTileObject => objects;
        private List<ITagObject> objects = new List<ITagObject>();
        public IList<string> Tags {
            get;
        }
        public void AddObject(ITagObject obj)
        {
            objects.Add(obj);
        }
        public void RemoveObject(ITagObject obj)
        {
            objects.Remove(obj);
        }
        public IReadOnlyList<ITagObject> GetSelfAndAllObjects()
        {
            var temp= GameTileObject.Select(a => a as ITagObject).ToList();
            temp.Add(this);
            return temp;
        }
    }
}