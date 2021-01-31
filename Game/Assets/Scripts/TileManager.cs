using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    //Spawn
    public GameObject[] roomPrefabs; //all tiles
    public List<Transform> currentRoomSpawners;

    public GameObject LastRoom;
    public GameObject thisRoom;
    public GameObject currentroom;

    public List<GameObject> activeRooms; //list to store all loaded tiles, to be able to delete

    void Start()
    {
        NewStart();
    }
    private void NewStart()
    {
        currentRoomSpawners.Clear();
        activeRooms.Clear();
        for (int i = 0; i < thisRoom.transform.GetChild(0).childCount; i++)
        {
            if (LastRoom == null)
            {
                currentRoomSpawners.Add(thisRoom.transform.GetChild(0).GetChild(i));
            }
            else
            {
                if (thisRoom.transform.GetChild(0).GetChild(i).transform.position != LastRoom.transform.position)
                {
                    currentRoomSpawners.Add(thisRoom.transform.GetChild(0).GetChild(i));
                }
            }

        }
        SpawnRoom();
    }

    void SpawnRoom()
    {
        for (int i = 0; i < currentRoomSpawners.Count; i++)
        {
            GameObject g_obj;
            g_obj = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)]) as GameObject; //call random tile
            g_obj.transform.SetParent(transform); //add it under tilemanager in hierarchy
            g_obj.transform.position = currentRoomSpawners[i].position;
            //place the called tile
            activeRooms.Add(g_obj); //add it to the list above
        }
    }
    public void roomCleaner()
    {
        for (int i = 0; i < currentRoomSpawners.Count; i++)
        {
            if (currentroom.transform.position != activeRooms[i].transform.position)
            {
                //SHIT CODE
                activeRooms[i].transform.GetChild(4).transform.GetChild(8).tag = "deadground";
                Destroy(activeRooms[i]);
            }
        }

        if (LastRoom != null)
        {
            //SHIT CODE
            LastRoom.transform.GetChild(4).transform.GetChild(8).tag = "deadground";
            Destroy(LastRoom);
        }
        LastRoom = thisRoom;
        thisRoom = currentroom;
        NewStart();

    }
}
