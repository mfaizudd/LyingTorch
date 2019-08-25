using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTile : MonoBehaviour
{
    public CorridorTile[] corridorSpawns;
    public Transform[] roomSpawns;
    public GameObject roomPrefab;
    public float roomCheckRadius = 1f;

    public int RoomLeft
    {
        get
        {
            return corridorSpawns.Count(x => !x.HaveRoom);
        }
    }

    public bool CheckLocationAvailable(Transform transform)
    {
        var room = Physics2D.OverlapCircle(transform.position, roomCheckRadius);
        return room == null && room.CompareTag(roomPrefab.tag);
    }
}
