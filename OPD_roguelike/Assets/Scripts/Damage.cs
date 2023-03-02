using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private Collider _currentCollider;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health.HitToPlayer(1);
        }
    }

}
