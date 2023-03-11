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
    private List<GameObject> bossPrefab;
    [SerializeField]
    private List<GameObject> decorations;
    [SerializeField]
    private List<GameObject> chests;
    [SerializeField]
    private List<GameObject> mobs;

    [SerializeField]
    private Transform spawnPointBoss;
    [SerializeField]
    private Transform spawnPointChest;
    [SerializeField]
    private List<Transform> spawnPointDecorations;
    [SerializeField]
    private List<Transform> spawnPointMobs;

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
        for(int i = 0; i < spawnPointDecorations.Count; i++)
        {
            if(Random.Range(0, 5) < 4)
            {
                Instantiate(decorations[Random.Range(0, decorations.Count)],
                    spawnPointDecorations[i].position,
                    Quaternion.identity);
            }
        }
    }
    private void InstantiationMobs()
    {

    }
    private void InstantiationBoss()
    {

    }
    private void InstantiationChest()
    {

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
