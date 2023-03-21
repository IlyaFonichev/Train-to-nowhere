using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRoom : Room
{
    [SerializeField]
    private List<GameObject> chestsPrefabs;
    [SerializeField]
    private Transform spawnPointChest;
    public override void Instantiation()
    {
        Type = RoomType.Chest;
        Instantiate(chestsPrefabs[Random.Range(0, chestsPrefabs.Count)],
        spawnPointChest.position,
        Quaternion.identity).transform.SetParent(ObjectManager.transform);
        Destroy(spawnPointChest.gameObject);
    }
}
