using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public Transform spawnLocation;
    public RoomTile roomPrefab;
    public int iterations;
    public Torch torchPrefab;
    public TextMeshProUGUI interactionText;
    public Slider healthBar;
    public Monster monsterPrefab;
    public int maxMonstersPerRoom = 3;
    public GameObject loading;

    private List<RoomTile> rooms = new List<RoomTile>();

    private void Start()
    {
        StartCoroutine(InitializeWorld());
    }

    IEnumerator InitializeWorld()
    {
        loading.SetActive(true);
        GenerateRoom();
        GenerateExit();
        GenerateMonsterOnRooms();
        loading.SetActive(false);
        yield return null;
    }

    private void GenerateMonsterOnRooms()
    {
        bool first = true;
        foreach (var item in rooms)
        {
            if(first)
            {
                first = false;
                continue;
            }
            var totalMonster = Random.Range(0, maxMonstersPerRoom);
            for (int i = 0; i < totalMonster; i++)
            {
                Instantiate(monsterPrefab, item.transform.position, item.transform.rotation);
            }
        }
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

                if(rooms.Count % 5 == 0 && roomsGenerated.Count > 0)
                {
                    Instantiate(torchPrefab, roomsGenerated[0].transform.position, Quaternion.identity);
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
