using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    [SerializeField]
    private List<GameObject> bossPrefabs;
    [SerializeField]
    private Transform spawnPointBoss;

    public override void Instantiation()
    {
        Type = RoomType.Boss;
        GameObject boss = Instantiate(bossPrefabs[Random.Range(0, bossPrefabs.Count)],
                    spawnPointBoss.position,
                    Quaternion.identity);
        boss.transform.SetParent(ObjectManager.transform);
        MobsManager.instance.AddMob(boss);
        mobsInTheRoom.Add(boss);
        Destroy(spawnPointBoss.gameObject);
    }
}
