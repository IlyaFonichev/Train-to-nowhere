using UnityEngine;
using System.Collections.Generic;

[SelectionBase]
public abstract class Room : MonoBehaviour
{
    public SpriteRenderer Ground;
    [SerializeField]
    private GameObject originRoom;
    [SerializeField]
    private GameObject decorationPrefab;
    private GameObject objectManager;
    private Vector2 position;
    [HideInInspector]
    public List<GameObject> mobsInTheRoom;
    private GameObject topRoom, leftRoom, rightRoom, bottomRoom;
    private RoomType type;
    [SerializeField]
    private GameObject border;
    [SerializeField]
    private int sizeOffset;
    public enum RoomType
    {
        Start, 
        Enemy, 
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
    public void Coloring(Color roomColor)
    {
        Ground.gameObject.transform.localScale = new Vector3(Mathf.Pow(-1, Random.Range(0, 2)) * sizeOffset, 1 * sizeOffset, 1 * sizeOffset);
        Ground.color = roomColor;
        originRoom.GetComponent<OriginRoom>().Recolor(roomColor);
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
            RoomSwitcher.instance.HideDoorCollider(GetComponent<Room>().OriginRoom.gameObject, false);
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
    public int Size
    {
        get { return sizeOffset; }
    }
}
