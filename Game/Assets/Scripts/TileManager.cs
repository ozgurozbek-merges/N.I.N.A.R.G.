using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    //Spawn
    public GameObject[] tile_prefabs; //all tiles
    public Transform player_z;
    private float spawn_z = 65.0f; //tile spawn start location
    private float tile_lenght = 60.0f; //Just to cancel the bounce. Smaller value increases difficulty.
    private int allowed_tile_count = 12;

    //Delete
    public List<GameObject> active_tiles; //list to store all loaded tiles, to be able to delete

    void Start()
    {
        for (int i = 0; i < allowed_tile_count; i++) //load as many tiles as allowed tile count
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (player_z.position.z - (tile_lenght - 10) > (spawn_z - allowed_tile_count * tile_lenght))
        //tile_length - 10 is for deleting purposes.
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile()
    {
        GameObject g_obj;
        g_obj = Instantiate(tile_prefabs[Random.Range(0,tile_prefabs.Length)]) as GameObject; //call random tile
        g_obj.transform.SetParent(transform); //add it under tilemanager in hierarchy
        g_obj.transform.position = Vector3.forward * spawn_z; //place the called tile
        spawn_z += tile_lenght; //change next spawn location
        active_tiles.Add(g_obj); //add it to the list above
    }

    void DeleteTile()
    {
        Destroy(active_tiles[0]); //get oldest tile from the list and remove it from hierarchy
        active_tiles.RemoveAt(0); //remove it from the list
    }
}
