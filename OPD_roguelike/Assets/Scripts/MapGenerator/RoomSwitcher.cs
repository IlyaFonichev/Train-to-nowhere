using System.Collections.Generic;
using UnityEngine;

public class RoomSwitcher : MonoBehaviour
{
    private Vector3 startCameraPosition;
    private bool isInitialized = false;
    [SerializeField]
    private List<GameObject> rooms = null;
    [SerializeField]
    private GameObject currentRoom;
    public static RoomSwitcher instance;
    private bool roomContainMobs;

    private void Start()
    {
        roomContainMobs = false;
        SetInstance();
        startCameraPosition = Camera.main.transform.position - currentRoom.transform.position;
    }
    private void SetInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public bool Initialized
    {
        set { isInitialized = value; }
        get { return isInitialized; }
    }

    public List<GameObject> Rooms
    {
        set { rooms = value; }
    }

    public GameObject CurrentRoom
    {
        set { currentRoom = value; }
        get { return currentRoom; }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            PauseManager.instance.Pause();
        }
        if (isInitialized && !roomContainMobs /*&& !PauseManager.instance.onPause*/)
        {
            if (Input.GetKeyDown(KeyCode.S) && currentRoom.GetComponent<Room>().bottomNeighbor != null)
                Switch("BottomDoor", currentRoom.GetComponent<Room>().bottomNeighbor.tag);
            if (Input.GetKeyDown(KeyCode.A) && currentRoom.GetComponent<Room>().leftNeighbor != null)
                Switch("LeftDoor", currentRoom.GetComponent<Room>().leftNeighbor.tag);
            if (Input.GetKeyDown(KeyCode.D) && currentRoom.GetComponent<Room>().rightNeighbor != null)
                Switch("RightDoor", currentRoom.GetComponent<Room>().rightNeighbor.tag);
            if (Input.GetKeyDown(KeyCode.W) && currentRoom.GetComponent<Room>().topNeighbor != null)
                Switch("TopDoor", currentRoom.GetComponent<Room>().topNeighbor.tag);
        }
    }

    public void HideInactiveRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
            if (rooms[i] != currentRoom)
                rooms[i].SetActive(false);
    }
    private void Switch(string doorTag, string roomTag)
    {
        GameObject tempRoom;
        if(roomTag == "Room")
        {
            switch (doorTag)
            {
                case "BottomDoor":
                    tempRoom = currentRoom;
                    currentRoom = currentRoom.GetComponent<Room>().bottomNeighbor;
                    currentRoom.SetActive(true);
                    //Переход в следующую комнату
                    tempRoom.SetActive(false);
                    Minimap.instance.Move(0, -1);
                    break;
                case "TopDoor":
                    tempRoom = currentRoom;
                    currentRoom = currentRoom.GetComponent<Room>().topNeighbor;
                    currentRoom.SetActive(true);
                    //Переход в следующую комнату
                    tempRoom.SetActive(false);
                    Minimap.instance.Move(0, 1);
                    break;
                case "LeftDoor":
                    tempRoom = currentRoom;
                    currentRoom = currentRoom.GetComponent<Room>().leftNeighbor;
                    currentRoom.SetActive(true);
                    //Переход в следующую комнату
                    tempRoom.SetActive(false);
                    Minimap.instance.Move(-1, 0);
                    break;
                case "RightDoor":
                    tempRoom = currentRoom;
                    currentRoom = currentRoom.GetComponent<Room>().rightNeighbor;
                    currentRoom.SetActive(true);
                    //Переход в следующую комнату
                    tempRoom.SetActive(false);
                    Minimap.instance.Move(1, 0);
                    break;
                default:
                    break;
            }
            Camera.main.transform.position = currentRoom.transform.position + startCameraPosition;
        }
    }
    public bool RoomContainMobs
    {
        set { roomContainMobs = value; }
    }
}
