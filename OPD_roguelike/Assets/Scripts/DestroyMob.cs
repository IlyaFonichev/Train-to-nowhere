using UnityEngine;

public class DestroyMob : MonoBehaviour
{
    private void OnMouseDown()
    {
        MobsManager.instance.RemoveMob(gameObject);
    }
}
