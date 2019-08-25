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
        GenerateRoom(iterations);
    }

    public void GenerateRoom(int iteration, List<RoomTile> targetRoom = null)
    {
        if (iteration <= 0)
            return;

        if(targetRoom==null)
        {
            var currentRoom = Instantiate(roomPrefab, spawnLocation.position, spawnLocation.rotation);
            var roomsGenerated = currentRoom.RandomlySpawnRoom(1);
            iteration -= roomsGenerated.Count;
            GenerateRoom(iteration, roomsGenerated);
        }
        else
        {
            foreach (var item in targetRoom)
            {
                var roomsGenerated = item.RandomlySpawnRoom();
                iteration -= roomsGenerated.Count;
                GenerateRoom(iteration, roomsGenerated);
            }
        }
    }
}
