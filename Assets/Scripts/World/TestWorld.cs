using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWorld : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0, 128)]
    public int w, h, cellSize;
    public PGrid grid;
    public GameObject[] prefabWall;

    private Movement player;

    private void Start()
    {
        Random.InitState(Random.Range(0, 15000));
        int randomX = Random.Range(0, w);
        int randomY = Random.Range(0, h);
        grid = new PGrid(w, h, cellSize, prefabWall, transform.gameObject);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        Vector3 selectedSpawn = grid.GetWorldPosition(randomX, randomY);
        SpawnPlayer(selectedSpawn);

    }

    public void SpawnPlayer(Vector3 selectedSpawn)
    {
        if (grid.gridObjects[grid.GetWorldPosition((int)selectedSpawn.x, (int)selectedSpawn.y)] != (int)PGrid.SpaceSlot.Wall && grid.gridObjects[grid.GetWorldPosition((int)selectedSpawn.x, (int)selectedSpawn.y)] == (int)PGrid.SpaceSlot.Floor)
        {
            player.transform.position = selectedSpawn;
            Camera.main.transform.position = new Vector3(selectedSpawn.x, selectedSpawn.y, Camera.main.transform.position.z);
        }
        else
        {
            Debug.Log("Spawning player on top of wall");
            int randomX = Random.Range(0, w);
            int randomY = Random.Range(0, h);
            selectedSpawn = grid.GetWorldPosition(randomX, randomY);
            SpawnPlayer(selectedSpawn);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
