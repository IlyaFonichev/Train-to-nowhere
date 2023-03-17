using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveGenerator : MapGenerator
{
    [SerializeField]
    private GameObject exitDoorPrefab;
    private GameObject currentRoom;
    [SerializeField, Tooltip("0 - ChestRoom\n1 - MobsRoom")]
    private List<GameObject> roomPrefabs;
    public override void CountRoomInitialization()
    {
        CountOfRooms = 7;
    }
    public override void Generate()
    {
        currentRoom = rooms[0];
        while (CurrentCountOfRooms != CountOfRooms - 1)
        {
            int neighbor = Random.Range(0, 4);
            string currentDoorTag = "";
            switch (neighbor)
            {
                case 0:
                    if (currentRoom.GetComponent<Room>().topNeighbor == null)
                        currentDoorTag = "TopDoor";
                    else
                        currentDoorTag = "BottomDoor";
                    break;
                case 1:
                    if (currentRoom.GetComponent<Room>().bottomNeighbor == null)
                        currentDoorTag = "BottomDoor";
                    else
                        currentDoorTag = "LeftDoor";
                    break;
                case 2:
                    if (currentRoom.GetComponent<Room>().leftNeighbor == null)
                        currentDoorTag = "LeftDoor";
                    else
                        currentDoorTag = "RightDoor";
                    break;
                case 3:
                    if (currentRoom.GetComponent<Room>().rightNeighbor == null)
                        currentDoorTag = "RightDoor";
                    else
                        currentDoorTag = "TopDoor";
                    break;
                default:
                    break;
            }

            GameObject newRoom = InstantiateRoom();
            CurrentCountOfRooms++;
            rooms.Add(newRoom);
            newRoom.name += System.Convert.ToString(CurrentCountOfRooms);
            newRoom.GetComponent<Room>().Position = currentRoom.GetComponent<Room>().Position + SetOffsetVector(currentDoorTag);
            SetDoors(newRoom, currentRoom, currentDoorTag, true);

            currentRoom = newRoom;
        }
        InstantiateExitDoor();
    }

    public void InstantiateExitDoor()
    {
        int angle = 0;
        int neighbor = Random.Range(0, 4);
        GameObject exitDoor = null;
        string exitDoorTag = "";
        switch (neighbor)
        {
            case 0:
                if (rooms[0].GetComponent<Room>().topNeighbor == null)
                    exitDoorTag = "TopDoor";
                else
                    exitDoorTag = "BottomDoor";
                break;
            case 1:
                if (rooms[0].GetComponent<Room>().bottomNeighbor == null)
                    exitDoorTag = "BottomDoor";
                else
                    exitDoorTag = "TopDoor";
                break;
            case 2:
                angle = 90;
                if (rooms[0].GetComponent<Room>().leftNeighbor == null)
                    exitDoorTag = "LeftDoor";
                else
                    exitDoorTag = "RightDoor";
                break;
            case 3:
                angle = 90;
                if (rooms[0].GetComponent<Room>().rightNeighbor == null)
                    exitDoorTag = "RightDoor";
                else
                    exitDoorTag = "LeftDoor";
                break;
            default:
                break;
        }
        for (int i = 0; i < rooms[0].transform.childCount; i++)
            if (rooms[0].transform.GetChild(i).CompareTag(exitDoorTag))
                exitDoor = rooms[0].transform.GetChild(i).gameObject;
        Instantiate(exitDoorPrefab, exitDoor.transform.position, Quaternion.Euler(90, 0, angle)).transform.SetParent(rooms[0].transform);
    }

    public override GameObject InstantiateRoom()
    {
        if (CurrentCountOfRooms == CountOfRooms - 2)
            return Instantiate(roomPrefabs[0], Vector3.zero, Quaternion.Euler(90, 0, 0));
        else
            return Instantiate(roomPrefabs[1], Vector3.zero, Quaternion.Euler(90, 0, 0));
    }
}
