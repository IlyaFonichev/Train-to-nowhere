using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StaticEventsOfPlayer.heal?.Invoke(PlayerController.GetHealthOfPlayer());
            Destroy(gameObject);
        }
    }
}

