using DG.Tweening;
using UnityEngine;
using XLua;
namespace XLuaTest.GameLuaTest
{
    [LuaCallCSharp]
    public class GameMainCamera
    {
        public static Camera MainCamera = Camera.main;

        public static void FocusOnPosition(float x, float y)
        {
            MoveCamera(x,y);
        }
        public static void FocusOnActor(string actorUUID)
        {
            Debug.Log(actorUUID);
            MoveCamera (Map.GetCurrentMapInfo().Actors.Find(a => a.Uuid.Equals(actorUUID)).Position);
        }
        private static void MoveCamera(Vector2 position)
        {
            MoveCamera(position.x, position.y);
        }
        private static void MoveCamera(float x, float y)
        {
            Vector3 toPosition=new Vector3(x,3.77f,y) ;
            MainCamera.transform.DOMove(toPosition, 0.5f);
        }
    }
}