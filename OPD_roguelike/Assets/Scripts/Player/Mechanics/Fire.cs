using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //private Collider _currentCollider;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StaticEventsOfPlayer.takeDamage?.Invoke(PlayerController.GetHealthOfPlayer(), 1);
        }
    }
}
