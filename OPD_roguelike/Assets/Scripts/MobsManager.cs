using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject portalPrefab;
    [SerializeField]
    private List<GameObject> mobs;
    public static MobsManager instance;
    private bool portalIsNeed;
    private void Awake()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Laboratory" || sceneName == "Forest" || sceneName == "Wasteland" || sceneName == "OldDange")
            portalIsNeed = true;
        else

            portalIsNeed = false;
        SetInstance();
    }

    private void SetInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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
        if (mobs.Count == 0 && portalIsNeed)
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
