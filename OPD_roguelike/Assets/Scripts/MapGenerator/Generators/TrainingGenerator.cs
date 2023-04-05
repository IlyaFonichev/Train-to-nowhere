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
            AddLoadSceneIndicator();
            int numberOfCurrentParentDoor = Random.Range(0, doors.Count);
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
        InstantiateExitDoor(exitDoorPrefab);
    }
    public override GameObject InstantiateRoom()
    {
        return Instantiate(EmptyRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
    }
}
