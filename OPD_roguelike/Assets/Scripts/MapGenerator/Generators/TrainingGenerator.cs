using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingGenerator : MapGenerator
{
    [SerializeField]
    private GameObject exitDoorPrefab;
    public override void CountRoomInitialization()
    {
        CountOfRooms = 3;
    }
    public override void Generate()
    {
        while (CurrentCountOfRooms != CountOfRooms - 1)
        {
            int numberOfCurrentParentDoor = Random.Range(0, doors.Count);
            GameObject currentParentDoor = doors[numberOfCurrentParentDoor];
            Vector2 positionOffset = SetOffsetVector(currentParentDoor.tag);

            GameObject newRoom = InstantiateRoom();
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
                newRoom.name += System.Convert.ToString(CurrentCountOfRooms);
                SetDoors(newRoom, parentRoom, currentParentDoor.tag, true);
                CurrentCountOfRooms++;
            }
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
        return Instantiate(EmptyRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
    }
}
