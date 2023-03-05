using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private Vector2 position;
    [SerializeField]
    private GameObject topRoom, leftRoom, rightRoom, bottomRoom;
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
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
}
