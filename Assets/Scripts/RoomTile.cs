using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTile : MonoBehaviour
{
    public CorridorTile[] corridorSpawns;
    public Transform[] roomSpawns;
    public RoomTile roomPrefab;
    public float roomCheckRadius = 1f;
    public LayerMask roomLayer;

    private Dictionary<Direction, CorridorTile> corridorDictionary = new Dictionary<Direction, CorridorTile>();

    private void Awake()
    {
        foreach (var item in corridorSpawns)
        {
            corridorDictionary.Add(item.direction, item);
            item.HaveRoom = false;
        }
    }

    public int RoomLeft
    {
        get
        {
            return corridorSpawns.Count(x => !x.HaveRoom);
        }
    }

    public List<CorridorTile> GetEmptyLocations()
    {
        List<CorridorTile> rooms = new List<CorridorTile>();
        foreach (var item in corridorSpawns)
        {
            if (item.HaveRoom)
                continue;
            if (!CheckLocationIsEmpty(item.roomSpawn.transform))
                continue;
            rooms.Add(item);
        }
        return rooms;
    }

    public List<RoomTile> RandomlySpawnRoom(int min = 0)
    {
        var available = GetEmptyLocations();
        int roomCount = Random.Range(Mathf.Min(min, available.Count), available.Count);
        var generatedRoom = new List<RoomTile>();
        for (int i = 0; i < roomCount; i++)
        {
            var spawnOn = available[Random.Range(0, available.Count)];
            available.Remove(spawnOn);
            var spawnedRoom = Instantiate(roomPrefab, spawnOn.roomSpawn.transform.position, Quaternion.identity);
            spawnOn.HaveRoom = true;
            spawnedRoom.GetOppositeCorridor(spawnOn.direction).HaveRoom = true;
            generatedRoom.Add(spawnedRoom);
        }
        return generatedRoom;
    }

    public bool SpawnExit()
    {
        var available = GetEmptyLocations();
        if(available.Any())
        {
            var exitOn = available[Random.Range(0, available.Count)];
            exitOn.GetComponent<SpriteRenderer>().color = Color.red;
            exitOn.gameObject.SetActive(true);
            return true;
        }
        return false;
    }

    public bool CheckLocationIsEmpty(Transform transform)
    {
        var room = Physics2D.OverlapCircle(transform.position, roomCheckRadius, roomLayer);
        if (room != null)
            return !room.CompareTag(roomPrefab.tag);
        else
            return true;
    }

    public bool CheckAnyLocationAvailable()
    {
        foreach (var item in roomSpawns)
        {
            if (!CheckLocationIsEmpty(item)) return false;
        }
        return true;
    }

    public CorridorTile GetOppositeCorridor(Direction direction)
    {
        return corridorDictionary[direction].oppositeCorridor;
    }
}
