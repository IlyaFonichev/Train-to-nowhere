using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    private GameObject objectManager;
    private Vector2 position;
    [SerializeField]
    private GameObject topRoom, leftRoom, rightRoom, bottomRoom;

    [Space(20)]
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

    [SerializeField]
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
        InstantiateObjectManager();
        InstantiationDecorations();
        switch (type)
        {
            case TypeRoom.Chest:
                InstantiationChest();
                break;
            case TypeRoom.Boss:
                InstantiationBoss();
                break;
            case TypeRoom.Mobs:
                InstantiationMobs();
                break;
            default:
                break;
        }
        DestroyOtherSpawnPoints();
    }

    private void InstantiateObjectManager()
    {
        objectManager = new GameObject("ObjectManager");
        objectManager.transform.SetParent(transform);
    }

    private void DestroyOtherSpawnPoints()
    {
        for (int i = 0; i < spawnPointsDecorations.Count; i++)
            Destroy(spawnPointsDecorations[i].gameObject);
        for (int i = 0; i < spawnPointsMobs.Count; i++)
            Destroy(spawnPointsMobs[i].gameObject);
        Destroy(spawnPointChest.gameObject);
        Destroy(spawnPointBoss.gameObject);
        spawnPointsDecorations.Clear();
        spawnPointsMobs.Clear();
    }

    private void InstantiationDecorations()
    {
        for (int i = 0; i < spawnPointsDecorations.Count; i++)
        {
            if (Random.Range(0, 5) < 4)
            {
                Instantiate(decorationsPrefabs[Random.Range(0, decorationsPrefabs.Count)],
                    spawnPointsDecorations[i].position,
                    Quaternion.identity).transform.SetParent(objectManager.transform);
            }
        }
    }
    private void InstantiationMobs()
    {
        for (int i = 0; i < spawnPointsMobs.Count; i++)
        {
            if (Random.Range(0, 5) < 4)
            {
                GameObject newMob = Instantiate(mobsPrefabs[Random.Range(0, mobsPrefabs.Count)],
                    spawnPointsMobs[i].position,
                    Quaternion.identity);
                newMob.transform.SetParent(objectManager.transform);
                MobsManager.instance.AddMob(newMob);
            }
        }
    }
    private void InstantiationBoss()
    {
        GameObject boss = Instantiate(bossPrefabs[Random.Range(0, bossPrefabs.Count)],
                    spawnPointBoss.position,
                    Quaternion.identity);
        boss.transform.SetParent(objectManager.transform);
        MobsManager.instance.AddMob(boss);
    }

    private void InstantiationChest()
    {
        Instantiate(chestsPrefabs[Random.Range(0, chestsPrefabs.Count)],
                    spawnPointChest.position,
                    Quaternion.identity).transform.SetParent(objectManager.transform);
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
