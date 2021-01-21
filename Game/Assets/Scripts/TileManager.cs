using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    //Spawn
    public GameObject[] roomPrefabs; //all tiles
    private int allowedRoomCount = 4;
    public List<Transform> currentRoomSpawners;
    public GameObject thisRoom;

    //Delete
    public List<GameObject> activeRooms; //list to store all loaded tiles, to be able to delete

    void Start()
    {
        for (int i = 0; i < thisRoom.transform.GetChild(0).childCount; i++)
        {
            currentRoomSpawners.Add(thisRoom.transform.GetChild(0).GetChild(i));
        }
        SpawnRoom();
    }

    void SpawnRoom()
    {
        for (int i = 0; i < currentRoomSpawners.Count; i++)
        {
            GameObject g_obj;
            g_obj = Instantiate(roomPrefabs[Random.Range(0,roomPrefabs.Length)]) as GameObject; //call random tile
            g_obj.transform.SetParent(transform); //add it under tilemanager in hierarchy
            g_obj.transform.position = currentRoomSpawners[i].position; //place the called tile
            activeRooms.Add(g_obj); //add it to the list above
        }
    }
}
