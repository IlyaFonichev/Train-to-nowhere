using System.Collections;
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
    private uint countOfRooms = 1;
    private uint currentCountOfRooms = 0;
    [SerializeField]
    private const float verticalRoomOffset = 12f, horizontalRoomOffset = 19f;

    private void Start()
    {
        Initialized();
        StartCoroutine(Generate());
    }

    private void Initialized()
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

    IEnumerator Generate()
    {
        while (currentCountOfRooms != countOfRooms - 1)
        {
            Vector2 positionOffset = Vector2.zero;
            int numberOfCurrentParentDoor = Random.Range(0, doors.Count);
            GameObject currentParentDoor = doors[numberOfCurrentParentDoor];
            positionOffset = SetOffsetVector(currentParentDoor.tag);

            GameObject newRoom = Instantiate(roomPrefab, Vector3.zero, Quaternion.Euler(90, 0, 0));
            GameObject parentRoom = currentParentDoor.transform.parent.gameObject;
            newRoom.GetComponent<Room>().Position = parentRoom.GetComponent<Room>().Position + positionOffset;
            GameObject existingRoom = null;
            bool intersection = false;

            //Обработка пересечения
            for(int i = 0; i < rooms.Count; i++)
            {
                if(newRoom.GetComponent<Room>().Position == rooms[i].GetComponent<Room>().Position)
                {
                    existingRoom = rooms[i];
                    intersection = true;
                    break;
                }
            }

            if(intersection)
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
            yield return new WaitForSeconds(0.01f);
        }
        DestroyEmptyDoors();
        PlacementRoomPosition();
        SetParent();
        InstantiateRoomSwitcher();
        gameObject.name = "Map";
        Destroy(gameObject.GetComponent<LevelGenerator>());
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
        Debug.Log("Управление передано");
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
                if (!currentRoom.transform.GetChild(i).CompareTag("Untagged"))
                    doors.Add(currentRoom.transform.GetChild(i).gameObject);
        }
    }

    private void DestroyEmptyDoors()
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            for(int j = 0; j < rooms[i].transform.childCount; j++)
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
