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

    private void Start()
    {
        GenerateRoom();
    }

    public void GenerateRoom(List<RoomTile> targetRoom = null)
    {
        if (iterations <= 0)
            return;

        if(targetRoom==null)
        {
            var currentRoom = Instantiate(roomPrefab, spawnLocation.position, spawnLocation.rotation);
            var roomsGenerated = currentRoom.RandomlySpawnRoom(1);
            iterations -= roomsGenerated.Count;
            GenerateRoom(roomsGenerated);
        }
        else if (iterations > 0)
        {
            foreach (var item in targetRoom)
            {
                var roomsGenerated = item.RandomlySpawnRoom(1);
                iterations -= roomsGenerated.Count;
                GenerateRoom(roomsGenerated);
            }
        }
    }
}
