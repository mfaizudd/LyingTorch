using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorTile : MonoBehaviour
{
    public bool HaveRoom
    {
        get => connectedRoom != null;
    }
    private RoomTile connectedRoom;
    public RoomTile ConnectedRoom
    {
        get => connectedRoom;
        set
        {
            connectedRoom = value;
            gameObject.SetActive(value != null);
            barrier.SetActive(value == null);
        }
    }

    public Direction direction;
    public Transform roomSpawn;
    public CorridorTile oppositeCorridor;
    public GameObject barrier;
}

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}