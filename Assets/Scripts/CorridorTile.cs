using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorTile : MonoBehaviour
{
    public bool HaveRoom { get; set; }

    public Direction direction;

    public Transform roomSpawn;
}

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}