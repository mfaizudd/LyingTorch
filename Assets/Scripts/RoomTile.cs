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

    private Dictionary<Direction, CorridorTile> corridorDictionary = new Dictionary<Direction, CorridorTile>();

    private void Awake()
    {
        foreach (var item in corridorSpawns)
        {
            corridorDictionary.Add(item.direction, item);
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
        int roomCount = Random.Range(min, available.Count);
        var generatedRoom = new List<RoomTile>();
        for (int i = 0; i < roomCount; i++)
        {
            var spawnOn = available[Random.Range(0, available.Count)];
            available.Remove(spawnOn);
            var spawnedRoom = Instantiate(roomPrefab, spawnOn.roomSpawn.transform.position, spawnOn.roomSpawn.transform.rotation);
            spawnOn.HaveRoom = true;
            spawnOn.gameObject.SetActive(true);
            spawnedRoom.GetOppositeDirection(spawnOn.direction).HaveRoom = true;
            generatedRoom.Add(spawnedRoom);
        }
        return generatedRoom;
    }

    public bool CheckLocationIsEmpty(Transform transform)
    {
        var room = Physics2D.OverlapCircle(transform.position, roomCheckRadius);
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

    public CorridorTile GetOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return corridorDictionary[Direction.Down];
            case Direction.Right:
                return corridorDictionary[Direction.Left];
            case Direction.Down:
                return corridorDictionary[Direction.Up];
            case Direction.Left:
                return corridorDictionary[Direction.Right];
            default:
                return null;
        }
    }
}
