using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldGenerator : MapGenerator
{
    [SerializeField, Tooltip("0 - BossRoom\n1 - ChestRoom\n2 - MobsRoom")]
    private List<GameObject> roomPrefabs;
    public override void CountRoomInitialization()
    {
        CountOfRooms = (uint)PlayerPrefs.GetInt("Depth") + 5 + (uint)Random.Range(0, 3);
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
    }
    public override GameObject InstantiateRoom()
    {
        int numberRoom = Random.Range(0, 5);
        if (CurrentCountOfRooms == CountOfRooms - 2)
            return Instantiate(roomPrefabs[0], Vector3.zero, Quaternion.Euler(90, 0, 0));
        if (CurrentCountOfRooms == CountOfRooms - 3)
            return Instantiate(roomPrefabs[1], Vector3.zero, Quaternion.Euler(90, 0, 0));
        else
        {
            switch (numberRoom)
            {
                case 0:
                    return Instantiate(roomPrefabs[1], Vector3.zero, Quaternion.Euler(90, 0, 0));
                default:
                    int number = Random.Range(0, 5);
                    switch (number)
                    {
                        case 0:
                            return Instantiate(EmptyRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
                        default:
                            return Instantiate(roomPrefabs[2], Vector3.zero, Quaternion.Euler(90, 0, 0));
                    }
            }
        }
    }
}
