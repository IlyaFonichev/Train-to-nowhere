using System.Collections.Generic;
using UnityEngine;

public class RoomSwitcher : MonoBehaviour
{
    private bool isInitialized = false;
    [SerializeField]
    private List<GameObject> rooms = null;
    [SerializeField]
    private GameObject currentRoom;
    public static RoomSwitcher instance;

    private void Start()
    {
        SetInstance();
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
        GameObject tempRoom;
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
        HideDoorCollider(currentRoom.GetComponent<Room>().OriginRoom,
                !(currentRoom.GetComponent<Room>().Type != Room.RoomType.Boss && currentRoom.GetComponent<Room>().Type != Room.RoomType.Enemy));
        Camera.main.transform.position = currentRoom.transform.position + CameraController.instance.DeltaPosition + new Vector3(0, -4, 0);
        if(currentRoom.GetComponent<Room>().Type == Room.RoomType.Enemy || currentRoom.GetComponent<Room>().Type == Room.RoomType.Boss)
            MobActivate();
        CameraController.instance.SetCurrentRoom();
    }

    private void MobActivate()
    {
        List<GameObject> mobsInTheRoom = currentRoom.GetComponent<Room>().mobsInTheRoom;
        for (int i = 0; i < mobsInTheRoom.Count; i++)
            mobsInTheRoom[i].GetComponent<OriginEnemy>().Spawn();
    }

    public void HideDoorCollider(GameObject originRoom, bool state)
    {
        Room room = currentRoom.GetComponent<Room>();
        for (int i = 0; i < originRoom.transform.childCount; i++)
        {
            GameObject tempDoor = originRoom.transform.GetChild(i).gameObject;
            if ((room.leftNeighbor != null && tempDoor.CompareTag("LeftDoor"))
                || (room.topNeighbor != null && tempDoor.CompareTag("TopDoor"))
                || (room.rightNeighbor != null && tempDoor.CompareTag("RightDoor"))
                || (room.bottomNeighbor != null && tempDoor.CompareTag("BottomDoor")))
            {
                for(int j = 0; j < tempDoor.transform.childCount; j++)
                {
                    if(tempDoor.transform.GetChild(j).CompareTag("DoorCollider"))
                    {
                        tempDoor.transform.GetChild(j).gameObject.SetActive(state);
                        break;
                    }
                }
            }
        }
    }
}
