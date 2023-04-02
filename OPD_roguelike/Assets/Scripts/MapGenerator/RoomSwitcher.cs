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

    private void Start()
    {
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
    }

    public void HideInactiveRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
            if (rooms[i] != currentRoom)
                rooms[i].SetActive(false);
    }
    public void Switch(string doorTag)
    {
        GameObject tempRoom = null;
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
        if(currentRoom.GetComponent<Room>().Type != Room.RoomType.Boss && currentRoom.GetComponent<Room>().Type != Room.RoomType.Mobs)
        {
            for (int i = 0; i < currentRoom.GetComponent<Room>().OriginRoom.transform.childCount; i++)
                HideDoorCollider(currentRoom.GetComponent<Room>().OriginRoom.transform.GetChild(i).gameObject, false);
        }
        else
        {
            for (int i = 0; i < currentRoom.GetComponent<Room>().OriginRoom.transform.childCount; i++)
                HideDoorCollider(currentRoom.GetComponent<Room>().OriginRoom.transform.GetChild(i).gameObject, true);
        }
        Camera.main.transform.position = currentRoom.transform.position + startCameraPosition;
    }

    public void HideDoorCollider(GameObject targetRoom, bool state)
    {
        for (int p = 0; p < targetRoom.transform.childCount; p++)
            if (targetRoom.transform.GetChild(p).CompareTag("DoorCollider"))
            {
                targetRoom.transform.GetChild(p).gameObject.SetActive(state);
                break;
            }
    }
}
