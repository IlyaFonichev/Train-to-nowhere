using System.Collections.Generic;
using UnityEngine;

public class MobsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject portalPrefab;
    [SerializeField]
    private List<GameObject> mobs;
    public static MobsManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject Portal
    {
        set { portalPrefab = value; }
    }
    public void RemoveMob(GameObject mob)
    {
        RoomSwitcher.getCurrentRoom.GetComponent<Room>().RemoveMob(mob);
        mobs.Remove(mob);
        Destroy(mob);
        if (mobs.Count == 0)
        {
            Instantiate(portalPrefab,
                RoomSwitcher.getCurrentRoom.transform.position,
                Quaternion.identity).transform.SetParent(RoomSwitcher.getCurrentRoom.transform);
            Destroy(transform.gameObject);
        }
    }
    public void AddMob(GameObject mob)
    {
        mobs.Add(mob);
    }
}
