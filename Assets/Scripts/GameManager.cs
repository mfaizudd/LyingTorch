using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public Transform spawnLocation;
    public RoomTile roomPrefab;
    public int iterations;

    private List<RoomTile> rooms = new List<RoomTile>();

    private void Start()
    {
        GenerateRoom();
        GenerateExit();
    }

    public void GenerateRoom(List<RoomTile> targetRoom = null)
    {
        if (iterations <= 0)
            return;

        if(targetRoom==null)
        {
            var currentRoom = Instantiate(roomPrefab, spawnLocation.position, spawnLocation.rotation);
            var roomsGenerated = currentRoom.RandomlySpawnRoom(1);
            rooms.AddRange(roomsGenerated);
            iterations -= roomsGenerated.Count;
            GenerateRoom(roomsGenerated);
        }
        else if (iterations > 0)
        {
            foreach (var item in targetRoom)
            {
                var roomsGenerated = item.RandomlySpawnRoom(1);
                rooms.AddRange(roomsGenerated);
                foreach (var room in roomsGenerated)
                {
                    room.GetComponent<SpriteRenderer>().color = Color.white;
                }

                if(rooms.Count % 5 == 0 && roomsGenerated.Count > 0)
                {
                    roomsGenerated[0].gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }

                iterations -= roomsGenerated.Count;
                GenerateRoom(roomsGenerated);
            }
        }
    }

    public void GenerateExit()
    {
        bool exitSpawned = false;

        int lastIndex = rooms.Count / 2;
        while(!exitSpawned && lastIndex >= 0)
        {
            exitSpawned = rooms[lastIndex].SpawnExit();
            lastIndex--;
        }

        lastIndex = rooms.Count / 2;
        while(!exitSpawned && lastIndex < rooms.Count)
        {
            exitSpawned = rooms[lastIndex].SpawnExit();
            lastIndex++;
        }
    }
}
