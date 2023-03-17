using System.Collections.Generic;
using UnityEngine;

public class MobsRoom : Room
{
    [SerializeField]
    private GameObject spawnPointsMobs;
    [SerializeField]
    private List<GameObject> mobsPrefabs;
    public override void Instantiation()
    {
        for (int i = 0; i < spawnPointsMobs.transform.childCount; i++)
        {
            if (Random.Range(0, 5) < 4)
            {
                GameObject newMob = Instantiate(mobsPrefabs[Random.Range(0, mobsPrefabs.Count)],
                    spawnPointsMobs.transform.GetChild(i).position,
                    Quaternion.identity);
                newMob.transform.SetParent(ObjectManager.transform);
                MobsManager.instance.AddMob(newMob);
                mobsInTheRoom.Add(newMob);
            }
        }
        Destroy(spawnPointsMobs);
    }
}
