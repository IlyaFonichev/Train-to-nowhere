using System.Collections.Generic;
using UnityEngine;

public abstract class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject startRoomPrefab;
    private GameObject startRoom;
    [HideInInspector]
    public List<GameObject> rooms, doors;
    [SerializeField]
    private GameObject emptyRoom;
    private uint countOfRooms;
    private uint currentCountOfRooms = 0;
    [SerializeField]
    private const float verticalRoomOffset = 12f, horizontalRoomOffset = 19f;
    public static MapGenerator instance;
    [HideInInspector]
    public GameObject existingRoom;
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
        GameObject originRoom = startRoom.GetComponent<Room>().OriginRoom;
        for (int i = 0; i < originRoom.transform.childCount; i++)
            if (originRoom.transform.GetChild(i).CompareTag("LeftDoor")
                || originRoom.transform.GetChild(i).CompareTag("BottomDoor")
                || originRoom.transform.GetChild(i).CompareTag("TopDoor")
                || originRoom.transform.GetChild(i).CompareTag("RightDoor"))
                doors.Add(originRoom.transform.GetChild(i).gameObject);
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
        GameObject targetRoom = currentRoom.GetComponent<Room>().OriginRoom;
        for (int i = 0; i < targetRoom.transform.childCount; i++)
        {
            if (!targetRoom.transform.GetChild(i).CompareTag(tag))
                doors.Add(targetRoom.transform.GetChild(i).gameObject);
        }
    }

    private void DestroyEmptyDoors()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            for (int j = 0; j < rooms[i].GetComponent<Room>().OriginRoom.transform.childCount; j++)
            {
                GameObject targetRoom = rooms[i].GetComponent<Room>().OriginRoom.transform.GetChild(j).gameObject;
                switch (targetRoom.tag)
                {
                    case "TopDoor":
                        if (rooms[i].GetComponent<Room>().topNeighbor == null)
                        {
                            doors.Remove(targetRoom);
                            DestroyDoorSprite(targetRoom, "TopDoor");
                        }
                        break;
                    case "BottomDoor":
                        if (rooms[i].GetComponent<Room>().bottomNeighbor == null)
                        {
                            doors.Remove(targetRoom);
                            DestroyDoorSprite(targetRoom, "BottomDoor");
                        }
                        break;
                    case "LeftDoor":
                        if (rooms[i].GetComponent<Room>().leftNeighbor == null)
                        {
                            doors.Remove(targetRoom);
                            DestroyDoorSprite(targetRoom, "LeftDoor");
                        }
                        break;
                    case "RightDoor":
                        if (rooms[i].GetComponent<Room>().rightNeighbor == null)
                        {
                            doors.Remove(targetRoom);
                            DestroyDoorSprite(targetRoom, "RightDoor");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        doors.Clear();
    }

    private void DestroyDoorSprite(GameObject targetRoom, string roomTag)
    {
        for (int p = 0; p < targetRoom.transform.childCount; p++)
            if (targetRoom.transform.GetChild(p).CompareTag("DoorSprite"))
            {
                Destroy(targetRoom.transform.GetChild(p).gameObject);
                break;
            }
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
        switcher.HideInactiveRooms();
        switcher.Initialized = true;
        for(int i = 0; i < startRoom.GetComponent<Room>().OriginRoom.transform.childCount; i++)
            switcher.HideDoorCollider(startRoom.GetComponent<Room>().OriginRoom.transform.GetChild(i).gameObject, false);
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

    public void InstantiateExitDoor(GameObject exitDoorPrefab)
    {
        int angle = 0;
        int neighbor = Random.Range(0, 4);
        GameObject exitDoor = null;
        GameObject exitRoomDoor = new GameObject("ExitDoor");
        string exitDoorTag = "";
        switch (neighbor)
        {
            case 0:
                if (rooms[0].GetComponent<Room>().topNeighbor == null)
                {
                    exitDoorTag = "TopDoor";
                    rooms[0].GetComponent<Room>().topNeighbor = exitRoomDoor;
                }
                else
                {
                    exitDoorTag = "BottomDoor";
                    rooms[0].GetComponent<Room>().bottomNeighbor = exitRoomDoor;
                }
                break;
            case 1:
                if (rooms[0].GetComponent<Room>().bottomNeighbor == null)
                {
                    exitDoorTag = "BottomDoor";
                    rooms[0].GetComponent<Room>().bottomNeighbor = exitRoomDoor;
                }
                else
                {
                    exitDoorTag = "TopDoor";
                    rooms[0].GetComponent<Room>().topNeighbor = exitRoomDoor;
                }
                break;
            case 2:
                angle = 90;
                if (rooms[0].GetComponent<Room>().leftNeighbor == null)
                {
                    exitDoorTag = "LeftDoor";
                    rooms[0].GetComponent<Room>().leftNeighbor = exitRoomDoor;
                }
                else
                {
                    exitDoorTag = "RightDoor";
                    rooms[0].GetComponent<Room>().rightNeighbor = exitRoomDoor;
                }
                break;
            case 3:
                angle = 90;
                if (rooms[0].GetComponent<Room>().rightNeighbor == null)
                {
                    exitDoorTag = "RightDoor";
                    rooms[0].GetComponent<Room>().rightNeighbor = exitRoomDoor;
                }
                else
                {
                    exitDoorTag = "LeftDoor";
                    rooms[0].GetComponent<Room>().leftNeighbor = exitRoomDoor;
                }
                break;
            default:
                break;
        }
        for (int i = 0; i < rooms[0].GetComponent<Room>().OriginRoom.transform.childCount; i++)
            if (rooms[0].GetComponent<Room>().OriginRoom.transform.GetChild(i).CompareTag(exitDoorTag))
                exitDoor = rooms[0].GetComponent<Room>().OriginRoom.transform.GetChild(i).gameObject;
        Instantiate(exitDoorPrefab, exitDoor.transform.position, Quaternion.Euler(90, 0, angle)).transform.SetParent(rooms[0].transform);
    }

    public void ProceduralGenerate(int numberOfCurrentParentDoor)
    {
        GameObject currentParentDoor = doors[numberOfCurrentParentDoor];
        Vector2 positionOffset = SetOffsetVector(currentParentDoor.tag);

        GameObject newRoom = InstantiateRoom();
        GameObject parentRoom = currentParentDoor.transform.parent.transform.parent.gameObject;
        newRoom.GetComponent<Room>().Position = parentRoom.GetComponent<Room>().Position + positionOffset;
        GameObject existingRoom = null;
        bool intersection = false;

        //Обработка пересечения
        for (int i = 0; i < rooms.Count; i++)
        {
            if (newRoom.GetComponent<Room>().Position == rooms[i].GetComponent<Room>().Position)
            {
                existingRoom = rooms[i];
                intersection = true;
                break;
            }
        }

        if (intersection)
        {
            Destroy(newRoom);
            newRoom = existingRoom;
        }
        else
        {
            rooms.Add(newRoom);
            newRoom.name += System.Convert.ToString(CurrentCountOfRooms);
            CurrentCountOfRooms++;
        }
        SetDoors(newRoom, parentRoom, currentParentDoor.tag, !intersection);
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
