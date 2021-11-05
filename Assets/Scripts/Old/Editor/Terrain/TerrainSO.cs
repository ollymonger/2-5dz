using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/*
[CreateAssetMenu(menuName ="Dungeons", fileName ="Generate")]
public class TerrainSO : ScriptableObject
{
    public Vector2 dungeonSize = new Vector2(64, 64);

    [Range(1,6)]
    public int toGenerate = 4;
    [Range(1, 4)]
    public int corridorThickness;

    public Dictionary<int, Dictionary<Vector2, GameObject>> dungeonList = new Dictionary<int, Dictionary<Vector2, GameObject>>();
    GameObject parentTileMap;
    GameObject border;
    public Sprite[] tiles;

    public void Clear()
    {
        dungeonList.Clear();
    }

    public void Generate()
    {
        parentTileMap = new GameObject("Dungeon Grid");

        parentTileMap.AddComponent<Grid>();
        border = new GameObject("BorderParent");
        border.transform.parent = parentTileMap.transform;
        dungeonList.Add(0, new Dictionary<Vector2, GameObject>());
        GenerateBorder(border);

    }

    public void GenerateBorder(GameObject parent)
    {
        for (int x = 0; x < dungeonSize.x; x++)
        {
            for (int y = 0; y < dungeonSize.y; y++)
            {
                if (x == 0)
                {
                    Vector3 spawn = new Vector3(x, y, 0);
                    GameObject obj = new GameObject("border@" + spawn);
                    obj.transform.position = spawn;
                    obj.AddComponent<SpriteRenderer>().sprite = tiles[0];
                    obj.transform.SetParent(parent.transform);
                }
                if (y == 0)
                {
                    Vector3 spawn = new Vector3(x, y, 0);
                    GameObject obj = new GameObject("border@" + spawn);
                    obj.transform.position = spawn;
                    obj.AddComponent<SpriteRenderer>().sprite = tiles[0];
                    obj.transform.SetParent(parent.transform);
                }
                if (x == dungeonSize.x - 1)
                {
                    Vector3 spawn = new Vector3(dungeonSize.x, y, 0);
                    GameObject obj = new GameObject("border@" + spawn);
                    obj.transform.position = spawn;
                    obj.AddComponent<SpriteRenderer>().sprite = tiles[0];
                    obj.transform.SetParent(parent.transform);
                }

                if (y == dungeonSize.y - 1)
                {
                    Vector3 spawn = new Vector3(x, dungeonSize.y, 0);
                    GameObject obj = new GameObject("border@" + spawn);
                    obj.transform.position = spawn;
                    obj.AddComponent<SpriteRenderer>().sprite = tiles[0];
                    obj.transform.SetParent(parent.transform);
                }
            }
        }


        Vector3 lastspawn = new Vector3(dungeonSize.x, dungeonSize.y, 0);
        GameObject lastobj = new GameObject("border@" + lastspawn);
        lastobj.transform.position = lastspawn;
        lastobj.AddComponent<SpriteRenderer>().sprite = tiles[0];
        lastobj.transform.SetParent(parent.transform);

        GenerateFloor();
    }

    public void GenerateFloor()
    {
        GameObject floorParent = new GameObject("FloorParent");
        floorParent.transform.SetParent(parentTileMap.transform);
        for(int x = 1; x < dungeonSize.x; x++) {
        
            for(int y = 1; y < dungeonSize.y; y++)
            {
                GameObject objtospawn = new GameObject("floor@" + new Vector2(x, y));
                objtospawn.transform.SetParent(floorParent.transform);
                objtospawn.AddComponent<SpriteRenderer>().sprite = tiles[1];

                objtospawn.transform.position = new Vector2(x, y);

            }
        
        }

    }
}
*/
