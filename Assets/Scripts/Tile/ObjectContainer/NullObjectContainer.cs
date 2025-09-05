using System.Collections.Generic;
using Tile.Base;
namespace Tile.ObjectContainer
{
    public class NullObjectContainer : IMapObjectContainer
    {

        public IList<string> Tags {
            get;
        }
        public IReadOnlyList<ITagObject> GetSelfAndAllObjects()
        {
            return null;
        }
        public IList<ITagObject> GameTileObject {
            get;
        }
        public void AddObject(ITagObject obj)
        {
            throw new System.NotImplementedException();
        }
        public void RemoveObject(ITagObject obj)
        {
            throw new System.NotImplementedException();
        }
    }
}