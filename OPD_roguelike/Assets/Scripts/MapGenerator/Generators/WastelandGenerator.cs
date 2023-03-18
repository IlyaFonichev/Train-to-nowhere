using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WastelandGenerator : MapGenerator
{
    private int numberOfCurrentParentDoor;
    [SerializeField]
    private GameObject bossRoom;
    [SerializeField]
    private GameObject chestRoom;
    [SerializeField]
    private GameObject mobsRoom;
    public override void CountRoomInitialization()
    {
        CountOfRooms = (uint)PlayerPrefs.GetInt("WastelandDepth") + 7 + (uint)Random.Range(0, 5);
    }

    public override void Generate()
    {
        numberOfCurrentParentDoor = 0;
        while (CurrentCountOfRooms != CountOfRooms - 1)
        {
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
            numberOfCurrentParentDoor++;
        }
    }

    public override GameObject InstantiateRoom()
    {
        if (CurrentCountOfRooms == CountOfRooms - 2)
        {
            if(PlayerPrefs.GetInt("WastelandDepth") % 5 == 4)
                return Instantiate(bossRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
            else
                return Instantiate(chestRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
        }
        else
        {
            int number = Random.Range(0, 5);
            switch (number)
            {
                case 0:
                    return Instantiate(EmptyRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
                default:
                    return Instantiate(mobsRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
            }
        }
    }
}
