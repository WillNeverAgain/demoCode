using System.Collections;
using System.Collections.Generic;
using System.Text;
using Tile.Data;
using Tile.EditorInterface;
using Tile.MapEditor.ScenceEditor;
using UniTest;
using UnityEngine;

public class TestDataWirter : MonoBehaviour
{
    [SerializeField]
    private SceneDataWriter write;
    [SerializeField]
    private MapData_SO _mapDataSo;
    [InspectorButton]
    public void Test()
    {
        // write.CreateMapAssets("Test");
        // var newV = new List<Vector2Int>()
        // {
        //     Vector2Int.right,Vector2Int.up,new Vector2Int(3,5)
        // };
        // // var creRes= write.CreateCellEnumerable(newV);
        // foreach (var v in creRes)
        // {
        //     Debug.Log(v.CellID);
        // }

        // write.SaveMapAssets();        
    }
    [InspectorButton]
    public void Check()
    {
        StringBuilder b = new StringBuilder();
        foreach (var data in _mapDataSo.mapData)
        {
            foreach (var cell in data)
            {
                if (cell != IGameMapCellData.NULL_DATA)
                {
                    b.Append("cell\t");
                }
                else
                {
                    b.Append("Null\t");
                }
            }
            b.AppendLine();
        }
        Debug.Log(b.ToString());
    }
}
