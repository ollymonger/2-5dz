using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CodeMonkey.Utils;

public class PGrid
{
    public int w;
    public int h;
    private float cellSize;
    private GameObject[] prefabObject;

    private GameObject parentObject;

    private int[,] gridArray;
    public Dictionary<Vector2, int> gridObjects = new Dictionary<Vector2, int>();
    private TextMesh[,] debugTextArray;

    public enum SpaceSlot
    {
        empty, Wall, Floor, Doorway
    }

    public bool IsGridComponentFloor(int x, int y)
    {
        if (gridArray[x, y] == (int)SpaceSlot.Floor)
        {
            return true;
        }
        return false;
    }

    public PGrid(int w, int h, float cellSize, GameObject[] prefabObject, GameObject parentObject)
    {
        this.w = w; this.h = h; this.cellSize = cellSize; this.prefabObject = prefabObject; this.parentObject = parentObject;

        gridArray = new int[w, h];
        debugTextArray = new TextMesh[w, h];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (x == 0)
                {
                    gridObjects[GetWorldPosition(x, y)] = (int)SpaceSlot.Wall;
                    GameObject obj = GameObject.Instantiate(prefabObject[0], GetWorldPosition(x, y), Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(0, 0, 90);
                    obj.transform.SetParent(parentObject.transform);

                }
                if (y == 0)
                {

                    gridObjects[GetWorldPosition(x, y)] = (int)SpaceSlot.Wall;
                    GameObject obj = GameObject.Instantiate(prefabObject[0], GetWorldPosition(x, y), Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(0, 0, 180);
                    obj.transform.SetParent(parentObject.transform);

                }
                if (y == gridArray.GetLength(1) - 1)
                {

                    gridObjects[GetWorldPosition(x, y)] = (int)SpaceSlot.Wall;
                    GameObject obj = GameObject.Instantiate(prefabObject[0], GetWorldPosition(x, y), Quaternion.identity);
                    obj.transform.SetParent(parentObject.transform);

                }

                if (x == gridArray.GetLength(0) - 1)
                {

                    gridObjects[GetWorldPosition(x, y)] = (int)SpaceSlot.Wall;
                    GameObject obj = GameObject.Instantiate(prefabObject[0], GetWorldPosition(x, y), Quaternion.identity);
                    obj.transform.rotation = Quaternion.Euler(0, 0, 270);
                    obj.transform.SetParent(parentObject.transform);


                }

                if (x == gridArray.GetLength(0) - 1 && y == gridArray.GetLength(1) - 1)
                {
                    gridObjects[GetWorldPosition(x, y)] = (int)SpaceSlot.Wall;
                    GameObject obj = GameObject.Instantiate(prefabObject[3], GetWorldPosition(x, y), Quaternion.identity);
                    obj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    // Corner wall, needs to be rotated to 180!
                    obj.transform.rotation = Quaternion.Euler(0, 0, -90);
                    obj.transform.SetParent(parentObject.transform);
                }


                if (x == 0 && y == 0)
                {
                    gridObjects[GetWorldPosition(x, y)] = (int)SpaceSlot.Wall;
                    GameObject obj = GameObject.Instantiate(prefabObject[3], GetWorldPosition(x, y), Quaternion.identity);
                    obj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    // Corner wall, at 0,0
                    obj.transform.rotation = Quaternion.Euler(0, 0, 90);
                    obj.transform.SetParent(parentObject.transform);
                }

                if (x == gridArray.GetLength(0) - 1 && y == 0)
                {
                    gridObjects[GetWorldPosition(x, y)] = (int)SpaceSlot.Wall;
                    GameObject obj = GameObject.Instantiate(prefabObject[3], GetWorldPosition(x, y), Quaternion.identity);
                    obj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    // Corner wall, at max x, 0 
                    obj.transform.rotation = Quaternion.Euler(0, 0, 180);
                    obj.transform.SetParent(parentObject.transform);
                }


                if (x == 0 && y == gridArray.GetLength(1) - 1)
                {
                    gridObjects[GetWorldPosition(x, y)] = (int)SpaceSlot.Wall;
                    GameObject obj = GameObject.Instantiate(prefabObject[3], GetWorldPosition(x, y), Quaternion.identity);
                    obj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    // Corner wall, at max x, 0 
                    obj.transform.rotation = Quaternion.Euler(0, 0, 0);
                    obj.transform.SetParent(parentObject.transform);
                }

                if (x > 0 && x < gridArray.GetLength(0) - 2 && y > 0 && y < gridArray.GetLength(0) - 2)
                {
                    gridObjects.Add(GetWorldPosition(x, y), 0);
                    //UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 5, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }

                if (x > 0 && x < gridArray.GetLength(0) && y > 0 && y < gridArray.GetLength(0))
                {

                    gridObjects[GetWorldPosition(x, y)] = (int)SpaceSlot.Floor;
                    GameObject obj = GameObject.Instantiate(prefabObject[1], GetWorldPosition(x, y), Quaternion.identity);
                }

            }
        }



        Debug.DrawLine(GetWorldPosition(0, h), GetWorldPosition(w, h), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(w, 0), GetWorldPosition(w, h), Color.white, 100f);

        GenerateRoomsWithinGrid(15);
        SpawnRandomWeaponsWithinGrid(15);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    private void SpawnRandomWeaponsWithinGrid(int times)
    {
        for (int i = 0; i < times; i++)
        {
            GameObject weaponObj = new GameObject("unassignedweapon");
            weaponObj.AddComponent<Weapon>();
            int randX = Random.Range(0, w);
            int randY = Random.Range(0, h);
            SpawnSpecific(GetWorldPosition(randX, randY), weaponObj);
        }
    }

    private void SpawnSpecific(Vector3 pos, GameObject weaponObj)
    {
        weaponObj.GetComponent<Weapon>().Spawn(GetWorldPosition((int)pos.x, (int)pos.y));
        weaponObj.tag = "Weapon";
    }
    private void GenerateRoomsWithinGrid(int times)
    {
        // Loop through grid array, select random X /Y starting pos;
        // Fill cells above X with 1

        Random.InitState(Random.Range(1, 100000));
        for (int i = 0; i < times; i++)
        {
            int Rand = 0;
            int randX = Random.Range(0, w - 6);
            int randY = Random.Range(0, h - 6);

            // Choose random starting point from the grid!
            Vector3 initialPoint = GetWorldPosition(randX, randY);
            if (Rand == 0)
            {
                bool exitChosenPX = false;
                bool exitChosenPY = false;
                bool exitChosenNX = false;
                bool exitChosenNY = false;
                for (int x = 0; x < 6; x++)
                {
                    for (int y = 0; y < 6; y++)
                    {
                        if (gridObjects.ContainsKey(GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)) && gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] != 1)
                        {
                            if (x == 0)
                            {
                                int randexit = Random.Range(1, 6);
                                if (!exitChosenPX && y == randexit)
                                {
                                    gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Doorway;
                                    exitChosenPX = true;
                                }
                                else
                                {
                                    GameObject obj = GameObject.Instantiate(prefabObject[0], new Vector3(initialPoint.x + x, initialPoint.y + y, 0), Quaternion.identity);
                                    obj.transform.SetParent(parentObject.transform);
                                    obj.transform.rotation = Quaternion.Euler(0, 0, 90);

                                    gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Wall;
                                }
                            }
                            if (y == 0)
                            {
                                int randexit = Random.Range(1, 6);
                                if (!exitChosenPY && x == randexit)
                                {
                                    gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Doorway;
                                    exitChosenPY = true;
                                }
                                else
                                {
                                    GameObject obj = GameObject.Instantiate(prefabObject[0], new Vector3(initialPoint.x + x, initialPoint.y + y, 0), Quaternion.identity);
                                    obj.transform.SetParent(parentObject.transform);

                                    obj.transform.rotation = Quaternion.Euler(0, 0, 180);
                                    gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Wall;
                                }
                            }
                            if (x == 5)
                            {
                                int randexit = Random.Range(1, 6);
                                if (!exitChosenNX && y == randexit)
                                {
                                    gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Doorway;
                                    exitChosenNX = true;
                                }
                                else
                                {
                                    GameObject obj = GameObject.Instantiate(prefabObject[0], new Vector3(initialPoint.x + x, initialPoint.y + y, 0), Quaternion.identity);
                                    obj.transform.SetParent(parentObject.transform);
                                    obj.transform.rotation = Quaternion.Euler(0, 0, 270);

                                    gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Wall;
                                }
                            }
                            if (y == 5)
                            {
                                int randexit = Random.Range(1, 6);
                                if (!exitChosenNY && x == randexit)
                                {
                                    gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Doorway;
                                    exitChosenNY = true;
                                }
                                else
                                {
                                    GameObject obj = GameObject.Instantiate(prefabObject[0], new Vector3(initialPoint.x + x, initialPoint.y + y, 0), Quaternion.identity);
                                    obj.transform.SetParent(parentObject.transform);

                                    gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Wall;
                                }
                            }

                            if (x == 5 && y == 5)
                            {
                                gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Wall;
                                GameObject obj = GameObject.Instantiate(prefabObject[3], GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y), Quaternion.identity);
                                // Corner wall, needs to be rotated to 180!
                                obj.transform.rotation = Quaternion.Euler(0, 0, -90);
                                obj.transform.SetParent(parentObject.transform);
                            }


                            if (x == 0 && y == 0)
                            {
                                gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Wall;
                                GameObject obj = GameObject.Instantiate(prefabObject[3], GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y), Quaternion.identity);
                                // Corner wall, at 0,0
                                obj.transform.rotation = Quaternion.Euler(0, 0, 90);
                                obj.transform.SetParent(parentObject.transform);
                            }

                            if (x == 5 && y == 0)
                            {
                                gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Wall;
                                GameObject obj = GameObject.Instantiate(prefabObject[3], GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y), Quaternion.identity);
                                // Corner wall, at max x, 0 
                                obj.transform.rotation = Quaternion.Euler(0, 0, 180);
                                obj.transform.SetParent(parentObject.transform);
                            }


                            if (x == 0 && y == 5)
                            {
                                gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Wall;
                                GameObject obj = GameObject.Instantiate(prefabObject[3], GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y), Quaternion.identity);
                                // Corner wall, at max x, 0 
                                obj.transform.rotation = Quaternion.Euler(0, 0, 0);
                                obj.transform.SetParent(parentObject.transform);
                            }

                            if (x > 0 && x < 5 && y > 0 && y < 5)
                            {
                                gridObjects[GetWorldPosition((int)initialPoint.x + x, (int)initialPoint.y + y)] = (int)SpaceSlot.Floor;
                                // Generate empty space..
                                GameObject obj = GameObject.Instantiate(prefabObject[1], new Vector3(initialPoint.x + x, initialPoint.y + y, 0), Quaternion.identity);
                            }
                        }


                    }
                }
            }
        }
        /*
        for(int i = 0; i < times; i++)
        {
            for (int x = randX; x < 10; x++)
            {
                for (int y = randX; y < 10; y++)
                {
                    int valueofPos = gridObjects[GetWorldPosition(x, y)];
                    if (valueofPos == 0)
                    {
                        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        obj.transform.position = GetWorldPosition(x, y);
                        gridObjects[GetWorldPosition(x, y)] = 1;
                    }
                }
            }

        }*/
    }

}
