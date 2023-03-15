using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject startRoom;
    [SerializeField]
    private List<GameObject> rooms;
    [SerializeField]
    private List<GameObject> doors;
    [SerializeField]
    private GameObject roomPrefab;
    [SerializeField]
    private uint countOfRooms;
    private uint currentCountOfRooms = 0;
    [SerializeField]
    private const float verticalRoomOffset = 12f, horizontalRoomOffset = 19f;
    private void Start()
    {
        PlayerPrefs.SetInt("Depth", 0);
        countOfRooms = (uint)PlayerPrefs.GetInt("Depth") + 5 + (uint)Random.Range(0, 3);
        Debug.Log("Количество комнат: " + countOfRooms);
        Initialization();
        Generate();
        DestroyEmptyDoors();
        PlacementRoomPosition();
        SetParent();
        InstantiateRoomSwitcher();
        CompletionRooms();
        gameObject.name = "Map";
        Destroy(gameObject.GetComponent<LevelGenerator>());
    }

    private void Initialization()
    {
        startRoom.GetComponent<Room>().Position = Vector2.zero;
        rooms.Add(startRoom.gameObject);
        for (int i = 0; i < startRoom.transform.childCount; i++)
            if (startRoom.transform.GetChild(i).CompareTag("LeftDoor")
                || startRoom.transform.GetChild(i).CompareTag("BottomDoor")
                || startRoom.transform.GetChild(i).CompareTag("TopDoor")
                || startRoom.transform.GetChild(i).CompareTag("RightDoor"))
                doors.Add(startRoom.transform.GetChild(i).gameObject);
    }

    private void Generate()
    {
        while (currentCountOfRooms != countOfRooms - 1)
        {
            int numberOfCurrentParentDoor = Random.Range(0, doors.Count);
            GameObject currentParentDoor = doors[numberOfCurrentParentDoor];
            Vector2 positionOffset = SetOffsetVector(currentParentDoor.tag);

            GameObject newRoom = Instantiate(roomPrefab, Vector3.zero, Quaternion.Euler(90, 0, 0));
            GameObject parentRoom = currentParentDoor.transform.parent.gameObject;
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
                SetDoors(existingRoom, parentRoom, currentParentDoor.tag, false);
            }
            else
            {
                rooms.Add(newRoom);
                newRoom.name += System.Convert.ToString(currentCountOfRooms);
                SetDoors(newRoom, parentRoom, currentParentDoor.tag, true);
                currentCountOfRooms++;
            }
        }
    }

    private void SetDoors(GameObject currentRoom, GameObject neighbor, string doorTag, bool flag)
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
        int countChest = Random.Range(1, rooms.Count / 5), currentChestCount = 0;
        int bossRoomNumber = rooms.Count - 1;
        rooms[0].GetComponent<Room>().Type = Room.TypeRoom.Start;
        rooms[bossRoomNumber].GetComponent<Room>().Type = Room.TypeRoom.Boss;
        rooms[0].GetComponent<Room>().Completion();
        rooms[bossRoomNumber].GetComponent<Room>().Completion();
        rooms[0].GetComponent<Room>().RoomSwitcherInicialized = true;
        rooms[bossRoomNumber].GetComponent<Room>().RoomSwitcherInicialized = true;
        for (int i = 1; i < bossRoomNumber; i++)
        {
            if (Random.Range(0, countChest + 1) > currentChestCount)
            {
                rooms[i].GetComponent<Room>().Type = Room.TypeRoom.Chest;
                currentChestCount++;
            }
            else
            {
                switch (Random.Range(0, 4))
                {
                    case 0:
                        rooms[i].GetComponent<Room>().Type = Room.TypeRoom.Empty;
                        break;
                    default:
                        rooms[i].GetComponent<Room>().Type = Room.TypeRoom.Mobs;
                        break;
                }
            }
            rooms[i].GetComponent<Room>().Completion();
            rooms[i].GetComponent<Room>().RoomSwitcherInicialized = true;
        }
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

    private Vector2 SetOffsetVector(string doorTag)
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
}
