using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    [SerializeField]
    private Vector2 position;
    [SerializeField]
    private GameObject topRoom, leftRoom, rightRoom, bottomRoom;

    //Через PlayerPrefs добавим несколько видов боссов
    [SerializeField]
    private List<GameObject> bossPrefabs;
    [SerializeField]
    private List<GameObject> decorationsPrefabs;
    [SerializeField]
    private List<GameObject> chestsPrefabs;
    [SerializeField]
    private List<GameObject> mobsPrefabs;

    [SerializeField]
    private Transform spawnPointBoss;
    [SerializeField]
    private Transform spawnPointChest;
    [SerializeField]
    private List<Transform> spawnPointsDecorations;
    [SerializeField]
    private List<Transform> spawnPointsMobs;

    private TypeRoom type;
    public enum TypeRoom
    { 
        Empty,
        Start,
        Mobs,
        Chest,
        Boss
    }
    public void Completion()
    {
        InstantiationDecorations();
        switch (type)
        {
            case TypeRoom.Empty:
                break;
            case TypeRoom.Chest:
                InstantiationChest();
                break;
            case TypeRoom.Boss:
                InstantiationBoss();
                break;
            case TypeRoom.Mobs:
                InstantiationMobs();
                break;
        }
    }

    private void InstantiationDecorations()
    {
        for(int i = 0; i < spawnPointsDecorations.Count; i++)
        {
            if(Random.Range(0, 5) < 4)
            {
                Instantiate(decorationsPrefabs[Random.Range(0, decorationsPrefabs.Count)],
                    spawnPointsDecorations[i].position,
                    Quaternion.identity);
            }
        }
    }
    private void InstantiationMobs()
    {
        for (int i = 0; i < spawnPointsMobs.Count; i++)
        {
            if (Random.Range(0, 5) < 4)
            {
                Instantiate(mobsPrefabs[Random.Range(0, mobsPrefabs.Count)],
                    spawnPointsMobs[i].position,
                    Quaternion.identity);
            }
        }
    }
    private void InstantiationBoss()
    {
        Instantiate(bossPrefabs[Random.Range(0, bossPrefabs.Count)],
                    spawnPointBoss.position,
                    Quaternion.identity);
    }
    private void InstantiationChest()
    {
        Instantiate(chestsPrefabs[Random.Range(0, chestsPrefabs.Count)],
                    spawnPointChest.position,
                    Quaternion.identity);
    }

    public TypeRoom Type
    {
        get { return type; }
        set { type = value; }
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
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
}
