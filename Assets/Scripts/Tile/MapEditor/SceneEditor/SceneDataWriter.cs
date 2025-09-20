using System;
using System.Collections;
using System.Collections.Generic;
using Tile.Data;
using Tile.EditorInterface;
using Tile.SO;
using UnityEditor;
using UnityEngine;
namespace Tile.MapEditor.ScenceEditor
{
    public class SceneDataWriter : MonoBehaviour  
    {
        private IMapDataWrite soMapDataWriter=new SOMapDataWriter();
        [SerializeField]
        private SceneMapEditorSystem sceneMapEditorSystem;
        public string name;
        List<SceneDataTileMark> _marks=new List<SceneDataTileMark>();
        private void Awake()
        {
            soMapDataWriter.CreateMapAssets(name);
            StartCoroutine(SaveWait());
        }
        public void CreateCell(SceneDataTileMark mark)
        {
            _marks.Add(mark);
        }
        IEnumerator SaveWait()
        {
            yield return new WaitForSeconds(1);
            Vector2Int minPos = sceneMapEditorSystem.ctx.WorldPosToCell(_marks[0].transform.position);
            foreach (var mm in _marks)
            {
                Vector2Int pos = sceneMapEditorSystem.ctx.WorldPosToCell(mm.transform.position);
                if (pos.x < minPos.x)
                {
                    minPos.x = pos.x;
                }
                if (pos.y < minPos.y)
                {
                    minPos.y = pos.y;
                }
            }
            Vector3 pass = sceneMapEditorSystem.ctx.CellPosToWorld(minPos);
            Debug.Log(pass);
            sceneMapEditorSystem.ctx.Initialize(pass);
            foreach (var mm in _marks)
            {
                Vector2Int ps = sceneMapEditorSystem.ctx.WorldPosToCell(mm.transform.position);
                Debug.Log(ps);
                soMapDataWriter.CreateCell(ps);
            }
            soMapDataWriter.SaveMapAssets();
        }
    }
}