using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    private GameObject doorManager;
    [SerializeField]
    private GameObject uiRoomPrefab;
    [SerializeField]
    private GameObject uiDoorPrefab;
    private float space;
    private List<GameObject> rooms;
    [SerializeField]
    private List<GameObject> uiRooms;
    private float width, height;
    public static Minimap instance;
    [SerializeField]
    private GameObject offsetPosition;
    private Vector2 maxPosition = Vector2.zero, minPosition = Vector2.zero;
    private int numberOfCurrentRoom;
    [SerializeField, Range(1f, 1.5f), Min(1)]
    private float changeScale;

    private void Awake()
    {
        doorManager = new GameObject("DoorManager");
        doorManager.transform.SetParent(offsetPosition.transform);
        numberOfCurrentRoom = 0;
        space = transform.GetComponent<RectTransform>().rect.width / 16;
        SetInstance();
        width = space * 4;
        height = width * 9 / 16;
        uiRoomPrefab.transform.localScale = new Vector2(width / uiRoomPrefab.transform.GetChild(0).GetComponent<RectTransform>().rect.width,
            height / uiRoomPrefab.transform.GetChild(0).GetComponent<RectTransform>().rect.height);
        uiDoorPrefab.transform.localScale = new Vector2(space / uiDoorPrefab.transform.GetChild(0).GetComponent<RectTransform>().rect.width,
            space / uiDoorPrefab.transform.GetChild(0).GetComponent<RectTransform>().rect.height);
    }

    private void SetInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void Move(int x, int y)
    {
        SwitchUIRoom();
        offsetPosition.GetComponent<RectTransform>().position -= new Vector3(x * (width + space), y * (height + space), 0);
    }

    private void SwitchUIRoom()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i] == RoomSwitcher.instance.CurrentRoom)
            {
                uiRooms[numberOfCurrentRoom].transform.localScale /= changeScale;
                uiRooms[i].transform.localScale *= changeScale;
                numberOfCurrentRoom = i;
                return;
            }
        }
    }

    public void DrawMap()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            GameObject newUIRoom = Instantiate(uiRoomPrefab,
                offsetPosition.transform.position + new Vector3(rooms[i].GetComponent<Room>().Position.x * (width + space), rooms[i].GetComponent<Room>().Position.y * (height + space), 0),
                Quaternion.identity);
            newUIRoom.transform.SetParent(offsetPosition.transform);
            switch (rooms[i].GetComponent<Room>().Type)
            {
                case (Room.RoomType.Boss):
                    newUIRoom.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                    break;
                case (Room.RoomType.Mobs):
                    newUIRoom.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                    break;
                case (Room.RoomType.Chest):
                    newUIRoom.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
                    break;
                default:
                    break;
            }
            if (uiRooms.Count == 0)
                newUIRoom.transform.localScale *= changeScale;
            uiRooms.Add(newUIRoom);
            if (rooms[i].GetComponent<Room>().Position.x < minPosition.x)
                minPosition = new Vector2(rooms[i].GetComponent<Room>().Position.x, minPosition.y);
            if (rooms[i].GetComponent<Room>().Position.y < minPosition.y)
                minPosition = new Vector2(minPosition.x, rooms[i].GetComponent<Room>().Position.y);
            if (rooms[i].GetComponent<Room>().Position.x > maxPosition.x)
                maxPosition = new Vector2(rooms[i].GetComponent<Room>().Position.x, maxPosition.y);
            if (rooms[i].GetComponent<Room>().Position.y > maxPosition.y)
                maxPosition = new Vector2(maxPosition.x, rooms[i].GetComponent<Room>().Position.y);
        }
        DrawDoors();
    }

    private void DrawDoors()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].GetComponent<Room>().topNeighbor != null)
            {
                Instantiate(uiDoorPrefab,
                uiRooms[i].transform.position + new Vector3(0, space / 2 + uiRoomPrefab.transform.localScale.y * uiRoomPrefab.transform.GetChild(0).GetComponent<RectTransform>().rect.height / 2, 0),
                Quaternion.identity).transform.SetParent(doorManager.transform);
            }
            if (rooms[i].GetComponent<Room>().bottomNeighbor != null)
            {
                Instantiate(uiDoorPrefab,
                uiRooms[i].transform.position - new Vector3(0, space / 2 + uiRoomPrefab.transform.localScale.y * uiRoomPrefab.transform.GetChild(0).GetComponent<RectTransform>().rect.height / 2, 0),
                Quaternion.identity).transform.SetParent(doorManager.transform);
            }
            if (rooms[i].GetComponent<Room>().rightNeighbor != null)
            {
                Instantiate(uiDoorPrefab,
                uiRooms[i].transform.position + new Vector3(space / 2 + uiRoomPrefab.transform.localScale.x * uiRoomPrefab.transform.GetChild(0).GetComponent<RectTransform>().rect.width / 2, 0, 0),
                Quaternion.identity).transform.SetParent(doorManager.transform);
            }
            if (rooms[i].GetComponent<Room>().leftNeighbor != null)
            {
                Instantiate(uiDoorPrefab,
                uiRooms[i].transform.position - new Vector3(space / 2 + uiRoomPrefab.transform.localScale.x * uiRoomPrefab.transform.GetChild(0).GetComponent<RectTransform>().rect.width / 2, 0, 0),
                Quaternion.identity).transform.SetParent(doorManager.transform);
            }
        }
    }

    public void CleanRoom()
    {
        uiRooms[numberOfCurrentRoom].transform.GetChild(0).GetComponent<Image>().color = Color.white;
    }

    public void PortalRoom()
    {
        uiRooms[numberOfCurrentRoom].transform.GetChild(0).GetComponent<Image>().color = Color.green;
    }

    public List<GameObject> SetRooms
    {
        set { rooms = value; }
    }
}