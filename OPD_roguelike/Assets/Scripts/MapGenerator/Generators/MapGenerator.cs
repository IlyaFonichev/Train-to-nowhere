using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public abstract class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject startRoomPrefab;
    private GameObject startRoom;
    [HideInInspector]
    public List<GameObject> rooms, doors;
    [SerializeField]
    private GameObject emptyRoom;
    [SerializeField]
    private uint countOfRooms;
    private uint currentCountOfRooms = 0;
    [SerializeField]
    private const float verticalRoomOffset = 12f, horizontalRoomOffset = 19f;
    public static MapGenerator instance;
    private void Start()
    {
        SetInstance();
        CountRoomInitialization();
        //Debug.Log("Количество комнат: " + countOfRooms);
        Initialization();
        Generate();
        DestroyEmptyDoors();
        PlacementRoomPosition();
        SetParent();
        InstantiateRoomSwitcher();
        CompletionRooms();

        Minimap.instance.SetRooms = rooms;
        Minimap.instance.DrawMap();
        gameObject.name = "Map";
        Destroy(gameObject.GetComponent<MapGenerator>());
    }
    private void SetInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Initialization()
    {
        startRoom = Instantiate(StartRoomPrefab, Vector3.zero, Quaternion.Euler(90, 0, 0));
        startRoom.GetComponent<Room>().Position = Vector2.zero;
        startRoom.GetComponent<Room>().Type = Room.RoomType.Start;
        rooms.Add(startRoom.gameObject);
        for (int i = 0; i < startRoom.transform.childCount; i++)
            if (startRoom.transform.GetChild(i).CompareTag("LeftDoor")
                || startRoom.transform.GetChild(i).CompareTag("BottomDoor")
                || startRoom.transform.GetChild(i).CompareTag("TopDoor")
                || startRoom.transform.GetChild(i).CompareTag("RightDoor"))
                doors.Add(startRoom.transform.GetChild(i).gameObject);
    }

    public abstract void Generate();

    public abstract void CountRoomInitialization();

    public abstract GameObject InstantiateRoom();

    public void SetDoors(GameObject currentRoom, GameObject neighbor, string doorTag, bool flag)
    {
        switch (doorTag)
        {
            case "TopDoor":
                if (flag)
                    AddDoorsOfNewRoom(currentRoom, "BottomDoor");
                neighbor.GetComponent<Room>().topNeighbor = currentRoom;
                currentRoom.GetComponent<Room>().bottomNeighbor = neighbor;
                break;
            case "BottomDoor":
                if (flag)
                    AddDoorsOfNewRoom(currentRoom, "TopDoor");
                neighbor.GetComponent<Room>().bottomNeighbor = currentRoom;
                currentRoom.GetComponent<Room>().topNeighbor = neighbor;
                break;
            case "LeftDoor":
                if (flag)
                    AddDoorsOfNewRoom(currentRoom, "RightDoor");
                neighbor.GetComponent<Room>().leftNeighbor = currentRoom;
                currentRoom.GetComponent<Room>().rightNeighbor = neighbor;
                break;
            case "RightDoor":
                if (flag)
                    AddDoorsOfNewRoom(currentRoom, "LeftDoor");
                neighbor.GetComponent<Room>().rightNeighbor = currentRoom;
                currentRoom.GetComponent<Room>().leftNeighbor = neighbor;
                break;
            default:
                break;
        }
    }

    private void AddDoorsOfNewRoom(GameObject currentRoom, string tag)
    {
        for (int i = 0; i < currentRoom.transform.childCount; i++)
        {
            if (!currentRoom.transform.GetChild(i).CompareTag(tag))
                doors.Add(currentRoom.transform.GetChild(i).gameObject);
        }
    }

    private void DestroyEmptyDoors()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            for (int j = 0; j < rooms[i].transform.childCount; j++)
            {
                switch (rooms[i].transform.GetChild(j).tag)
                {
                    case "TopDoor":
                        if (rooms[i].GetComponent<Room>().topNeighbor == null)
                        {
                            doors.Remove(rooms[i].transform.GetChild(j).gameObject);
                            Destroy(rooms[i].transform.GetChild(j).gameObject);
                        }
                        break;
                    case "BottomDoor":
                        if (rooms[i].GetComponent<Room>().bottomNeighbor == null)
                        {
                            doors.Remove(rooms[i].transform.GetChild(j).gameObject);
                            Destroy(rooms[i].transform.GetChild(j).gameObject);
                        }
                        break;
                    case "LeftDoor":
                        if (rooms[i].GetComponent<Room>().leftNeighbor == null)
                        {
                            doors.Remove(rooms[i].transform.GetChild(j).gameObject);
                            Destroy(rooms[i].transform.GetChild(j).gameObject);
                        }
                        break;
                    case "RightDoor":
                        if (rooms[i].GetComponent<Room>().rightNeighbor == null)
                        {
                            doors.Remove(rooms[i].transform.GetChild(j).gameObject);
                            Destroy(rooms[i].transform.GetChild(j).gameObject);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        doors.Clear();
    }

    private void CompletionRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
            rooms[i].GetComponent<Room>().Completion();
    }

    private void InstantiateRoomSwitcher()
    {
        GameObject roomSwitcher = new GameObject("RoomSwitcher");
        roomSwitcher.transform.position = Vector3.zero;
        roomSwitcher.AddComponent<RoomSwitcher>();
        RoomSwitcher switcher = roomSwitcher.GetComponent<RoomSwitcher>();
        switcher.CurrentRoom = startRoom;
        switcher.Rooms = rooms;
        roomSwitcher.GetComponent<RoomSwitcher>().HideInactiveRooms();
        switcher.Initialized = true;
    }

    public Vector2 SetOffsetVector(string doorTag)
    {
        Vector2 positionOffset = Vector2.zero;
        switch (doorTag)
        {
            case "TopDoor":
                positionOffset = Vector2.up;
                break;
            case "BottomDoor":
                positionOffset = Vector2.down;
                break;
            case "LeftDoor":
                positionOffset = Vector2.left;
                break;
            case "RightDoor":
                positionOffset = Vector2.right;
                break;
            default:
                break;
        }
        return positionOffset;
    }

    private void PlacementRoomPosition()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].transform.position = new Vector3(rooms[i].GetComponent<Room>().Position.x * horizontalRoomOffset,
                0,
                rooms[i].GetComponent<Room>().Position.y * verticalRoomOffset);
        }
    }

    private void SetParent()
    {
        for (int i = 0; i < rooms.Count; i++)
            rooms[i].transform.parent = gameObject.transform;
    }

    public uint CountOfRooms
    {
        get { return countOfRooms; }
        set { countOfRooms = value; }
    }
    public uint CurrentCountOfRooms
    {
        get { return currentCountOfRooms; }
        set { currentCountOfRooms = value; }
    }
    public GameObject EmptyRoom
    {
        get { return emptyRoom; }
    }
    public GameObject StartRoomPrefab
    {
        get { return startRoomPrefab; }
    }
}
