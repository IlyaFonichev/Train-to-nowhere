using UnityEngine;

public class Looting : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.useop.EventAddScore(50);
        }
    }
}
