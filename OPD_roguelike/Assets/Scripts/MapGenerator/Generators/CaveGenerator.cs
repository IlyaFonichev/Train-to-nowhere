using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveGenerator : MapGenerator
{
    [SerializeField]
    private GameObject exitDoorPrefab;
    private GameObject currentRoom;
    [SerializeField]
    private GameObject chestRoom;
    [SerializeField]
    private GameObject mobsRoom;
    public override void CountRoomInitialization()
    {
        CountOfRooms = 7;
    }
    public override void Generate()
    {
        currentRoom = rooms[0];
        while (CurrentCountOfRooms != CountOfRooms - 1)
        {
            AddLoadSceneIndicator();
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
            newRoom.GetComponent<Room>().Position = currentRoom.GetComponent<Room>().Position + SetOffsetVector(currentDoorTag);
            SetDoors(newRoom, currentRoom, currentDoorTag, true); 
            GameObject existingRoom = null;

            if (!IntersectionCheck(newRoom, out existingRoom))
            {
                rooms.Add(newRoom);
                newRoom.name += System.Convert.ToString(CurrentCountOfRooms);
                CurrentCountOfRooms++;
                currentRoom = newRoom;
            }
            else
            {
                Destroy(newRoom);
            }
        }
        InstantiateExitDoor(exitDoorPrefab);
    }

    public override GameObject InstantiateRoom()
    {
        if (CurrentCountOfRooms == CountOfRooms - 2)
            return Instantiate(chestRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
        else
            return Instantiate(mobsRoom, Vector3.zero, Quaternion.Euler(90, 0, 0));
    }
}
