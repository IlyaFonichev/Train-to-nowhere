using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Room : MonoBehaviour
{
    private bool leftNeighbour;
    private bool rightNeighbour;
    private bool topNeighbour;
    private bool bottomNeighbour;
    [SerializeField]
    private Type type;
    enum Type
    {
        Empty,
        Start,
        Chest,
        Mobs,
        Boss,
    }

    private void Start()
    {
        gameObject.transform.parent = new GameObject("Gang").transform;
        transform.AddComponent<BoxCollider>();
        GetComponent<BoxCollider>().isTrigger = true;
    }

}
