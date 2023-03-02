using UnityEngine;

public class Room : MonoBehaviour
{
    private GameObject prefab;
    private GameObject[] spawnPointsOfMobs;
    private GameObject spawnPointsChest;
    private GameObject[] spawnPointsOfDecorations;
    //.....
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
        switch (type)
        { 
            case Type.Empty:
                break;
            case Type.Start:
                for()
                    Instantiate(prefab);
                break;
        }
    }

}
