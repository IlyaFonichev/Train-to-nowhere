using System.Collections.Generic;
using UnityEngine;

public class RoomSwitcher : MonoBehaviour
{
    private bool isInitialized = false;
    [SerializeField]
    private List<GameObject> rooms = null;
    [SerializeField]
    private GameObject currentRoom;

    public bool Initialized
    {
        set
        {
            isInitialized = value;
        }
    }

    public List<GameObject> Rooms
    {
        set
        {
            rooms = value;
        }
    }

    public GameObject CurrentRoom
    {
        set
        {
            currentRoom = value;
        }
    }

    private void Update()
    {
        if (isInitialized)
        {
            if (Input.GetKeyDown(KeyCode.S))
                Switch("BottomDoor");
            if (Input.GetKeyDown(KeyCode.A))
                Switch("LeftDoor");
            if (Input.GetKeyDown(KeyCode.D))
                Switch("RightDoor");
            if (Input.GetKeyDown(KeyCode.W))
                Switch("TopDoor");
        }
    }

    public void HideInactiveRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
            if (rooms[i] != currentRoom)
                rooms[i].SetActive(false);
    }
    private void Switch(string doorTag)
    {
        switch (doorTag)
        {
            case "BottomDoor":
                if (currentRoom.GetComponent<Room>().bottomNeighbor != null)
                {
                    GameObject tempRoom = currentRoom;
                    currentRoom = currentRoom.GetComponent<Room>().bottomNeighbor;
                    currentRoom.SetActive(true);
                    //Переход в следующую комнату
                    tempRoom.SetActive(false);
                }
                break;
            case "TopDoor":
                if (currentRoom.GetComponent<Room>().topNeighbor != null)
                {
                    GameObject tempRoom = currentRoom;
                    currentRoom = currentRoom.GetComponent<Room>().topNeighbor;
                    currentRoom.SetActive(true);
                    //Переход в следующую комнату
                    tempRoom.SetActive(false);
                }
                break;
            case "LeftDoor":
                if (currentRoom.GetComponent<Room>().leftNeighbor != null)
                {
                    GameObject tempRoom = currentRoom;
                    currentRoom = currentRoom.GetComponent<Room>().leftNeighbor;
                    currentRoom.SetActive(true);
                    //Переход в следующую комнату
                    tempRoom.SetActive(false);
                }
                break;
            case "RightDoor":
                if (currentRoom.GetComponent<Room>().rightNeighbor != null)
                {
                    GameObject tempRoom = currentRoom;
                    currentRoom = currentRoom.GetComponent<Room>().rightNeighbor;
                    currentRoom.SetActive(true);
                    //Переход в следующую комнату
                    tempRoom.SetActive(false);
                }
                break;

            default:
                break;
        }
    }
}
