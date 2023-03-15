using UnityEngine;
using System.Collections.Generic;

[SelectionBase]
public class Room : MonoBehaviour
{
    private bool roomSwitcerInicialized = false;
    private GameObject objectManager;
    private Vector2 position;
    [SerializeField]
    private List<GameObject> mobsInTheRoom;

    [SerializeField, Header("Type of the room")]
    private TypeRoom type;

    [SerializeField, Header("Neighbors at the current room")]
    private GameObject topRoom;
    [SerializeField]
    private GameObject leftRoom, rightRoom, bottomRoom;

    [Header("Prefabs for spawn")]
    //Через PlayerPrefs добавим несколько видов боссов
    [SerializeField]
    private List<GameObject> bossPrefabs;
    [SerializeField]
    private List<GameObject> decorationsPrefabs;
    [SerializeField]
    private List<GameObject> chestsPrefabs;
    [SerializeField]
    private List<GameObject> mobsPrefabs;

    [SerializeField, Header("Spawn points")]
    private Transform spawnPointBoss;
    [SerializeField]
    private Transform spawnPointChest;
    [SerializeField]
    private List<Transform> spawnPointsDecorations;
    [SerializeField]
    private List<Transform> spawnPointsMobs;
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
    private void OnEnable()
    {
        if(roomSwitcerInicialized)
        {
            if (mobsInTheRoom.Count == 0)
                RoomSwitcher.instance.RoomContainMobs = false;
            else
                RoomSwitcher.instance.RoomContainMobs = true;
        }
    }

    public void RemoveMob(GameObject mob)
    {
        if (mobsInTheRoom.Count == 1)
            RoomSwitcher.instance.RoomContainMobs = false;
        mobsInTheRoom.Remove(mob);
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
                mobsInTheRoom.Add(newMob);
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
        mobsInTheRoom.Add(boss);
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
    public bool RoomSwitcherInicialized
    {
        set { roomSwitcerInicialized = value; }
    }
}
