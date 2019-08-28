using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorTile : MonoBehaviour
{
    private bool haveRoom = false;
    public bool HaveRoom
    {
        get => haveRoom;
        set
        {
            haveRoom = value;
            gameObject.SetActive(value);
            barrier.SetActive(!value);
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