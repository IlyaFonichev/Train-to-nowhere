using UnityEngine;
using System.Collections.Generic;

[SelectionBase]
public abstract class Room : MonoBehaviour
{
    [SerializeField]
    private GameObject originRoom;
    [SerializeField]
    private GameObject decorationPrefab;
    private GameObject objectManager;
    private Vector2 position;
    [HideInInspector]
    public List<GameObject> mobsInTheRoom;
    [SerializeField, Header("Neighbors at the current room")]
    private GameObject topRoom;
    [SerializeField]
    private GameObject leftRoom, rightRoom, bottomRoom;
    private RoomType type;
    [SerializeField]
    private GameObject border;
    public enum RoomType
    {
        Start, 
        Mobs, 
        Boss,
        Chest,
        Empty
    }

    public abstract void Instantiation();

    public void Completion()
    {
        InstantiateObjectManager();
        Instantiation();
        InstantiateDecoration();
    }
    private void InstantiateObjectManager()
    {
        objectManager = new GameObject("ObjectManager");
        objectManager.transform.SetParent(transform);
    }
    private void InstantiateDecoration()
    {
        GameObject decoration = Instantiate(decorationPrefab, transform.position, Quaternion.Euler(90, 0, 0));
        decoration.transform.SetParent(objectManager.transform);
        decoration.GetComponent<DecorationPart>().Instantiation();
    }

    public void RemoveMob(GameObject mob)
    {
        if (mobsInTheRoom.Count == 1)
        {
            for (int i = 0; i < GetComponent<Room>().OriginRoom.transform.childCount; i++) 
                RoomSwitcher.instance.HideDoorCollider(GetComponent<Room>().OriginRoom.transform.GetChild(i).gameObject, false);
            type = RoomType.Empty;
            Minimap.instance.CleanRoom();
        }
        mobsInTheRoom.Remove(mob);
    }

    public GameObject leftNeighbor
    {
        set { leftRoom = value; }
        get { return leftRoom; }
    }
    public GameObject rightNeighbor
    {
        set { rightRoom = value; }
        get { return rightRoom; }
    }
    public GameObject topNeighbor
    {
        set { topRoom = value; }
        get { return topRoom; }
    }
    public GameObject bottomNeighbor
    {
        set { bottomRoom = value; }
        get { return bottomRoom; }
    }
    public GameObject ObjectManager
    {
        get { return objectManager; }
    }
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public RoomType Type
    {
        get { return type; }
        set { type = value; }
    }
    public GameObject OriginRoom
    {
        get { return originRoom; }
    }
}
