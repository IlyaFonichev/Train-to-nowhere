using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> rooms;
    [SerializeField]
    private List<GameObject> doors;
    [SerializeField]
    private GameObject roomPrefab;
    [SerializeField]
    private uint countOfRooms = 1, currentCountOfRooms = 0;
    [SerializeField]
    private const float verticalRoomOffset = 12f, horizontalRoomOffset = 19f;
    private void Start()
    {
        GetComponent<Room>().Position = Vector2.zero;
        rooms.Add(gameObject);
        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).CompareTag("LeftDoor") || transform.GetChild(i).CompareTag("BottomDoor") || transform.GetChild(i).CompareTag("TopDoor") || transform.GetChild(i).CompareTag("RightDoor"))
                doors.Add(transform.GetChild(i).gameObject);
        StartCoroutine(Generate());
    }
    IEnumerator Generate()
    {
        while (currentCountOfRooms != countOfRooms - 1)
        {
            Vector2 positionOffset = Vector2.zero;
            int numberOfCurrentParentDoor = Random.Range(0, doors.Count);
            GameObject currentParentDoor = doors[numberOfCurrentParentDoor];
            switch (currentParentDoor.tag)
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
            GameObject currentRoom = Instantiate(roomPrefab, Vector3.zero, Quaternion.Euler(90, 0, 0));
            currentRoom.GetComponent<Room>().Position = currentParentDoor.transform.parent.GetComponent<Room>().Position + positionOffset;
            GameObject existingRoom = null;
            bool intersection = false;
            for (int i = 0; i < rooms.Count; i++)
                if (rooms[i].GetComponent<Room>().Position == currentRoom.GetComponent<Room>().Position)
                {
                    intersection = true;
                    existingRoom = rooms[i];
                    break;
                }
            if (intersection)
            {
                Debug.Log("Intersection!");
                Destroy(currentRoom);
                doors.RemoveAt(numberOfCurrentParentDoor);
                switch (currentParentDoor.tag)
                {
                    case "TopDoor":
                        currentParentDoor.transform.parent.GetComponent<Room>().topNeighbor = existingRoom;
                        existingRoom.GetComponent<Room>().bottomNeighbor = currentParentDoor.transform.parent.gameObject;
                        break;
                    case "BottomDoor":
                        currentParentDoor.transform.parent.GetComponent<Room>().bottomNeighbor = existingRoom;
                        existingRoom.GetComponent<Room>().topNeighbor = currentParentDoor.transform.parent.gameObject;
                        break;
                    case "LeftDoor":
                        currentParentDoor.transform.parent.GetComponent<Room>().leftNeighbor = existingRoom;
                        existingRoom.GetComponent<Room>().rightNeighbor = currentParentDoor.transform.parent.gameObject;
                        break;
                    case "RightDoor":
                        currentParentDoor.transform.parent.GetComponent<Room>().rightNeighbor = existingRoom;
                        existingRoom.GetComponent<Room>().leftNeighbor = currentParentDoor.transform.parent.gameObject;
                        break;
                }
            }
            else
            {
                currentRoom.name = currentRoom.name + System.Convert.ToString(currentCountOfRooms);
                currentCountOfRooms++;
                doors.RemoveAt(numberOfCurrentParentDoor);
                switch (currentParentDoor.tag)
                {
                    case "TopDoor":
                        ChangingDoors(currentRoom, "BottomDoor");
                        currentParentDoor.transform.GetComponentInParent<Room>().topNeighbor = currentRoom;
                        currentRoom.GetComponent<Room>().bottomNeighbor = currentParentDoor.transform.parent.gameObject;
                        break;
                    case "BottomDoor":
                        ChangingDoors(currentRoom, "TopDoor");
                        currentParentDoor.transform.GetComponentInParent<Room>().bottomNeighbor = currentRoom;
                        currentRoom.GetComponent<Room>().topNeighbor = currentParentDoor.transform.parent.gameObject;
                        break;
                    case "LeftDoor":
                        ChangingDoors(currentRoom, "RightDoor");
                        currentParentDoor.transform.GetComponentInParent<Room>().leftNeighbor = currentRoom;
                        currentRoom.GetComponent<Room>().rightNeighbor = currentParentDoor.transform.parent.gameObject;
                        break;
                    case "RightDoor":
                        ChangingDoors(currentRoom, "LeftDoor");
                        currentParentDoor.transform.GetComponentInParent<Room>().rightNeighbor = currentRoom;
                        currentRoom.GetComponent<Room>().leftNeighbor = currentParentDoor.transform.parent.gameObject;
                        break;
                }
                currentRoom.transform.SetParent(currentParentDoor.transform);
                rooms.Add(currentRoom);
            }
            yield return new WaitForSeconds(0.01f);
        }
        DestroyEmptyDoors();
        PlacementRoomPosition();
    }

    private void ChangingDoors(GameObject currentRoom, string tag)
    {
        for (int i = 0; i < currentRoom.transform.childCount; i++)
        {
            if (currentRoom.transform.GetChild(i).CompareTag(tag))
                Destroy(currentRoom.transform.GetChild(i).gameObject);
            else
            {
                if (!currentRoom.transform.GetChild(i).CompareTag("Untagged"))
                    doors.Add(currentRoom.transform.GetChild(i).gameObject);
            }
        }
    }
    private void DestroyEmptyDoors()
    {
        for(int i = 0; i < doors.Count; i++)
            if (doors[i].transform.childCount <= 1)
                Destroy(doors[i]);
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
}
