using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //vrode tak nado
    private Collider _currentCollider;

    //pri sto9nii v ogne
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //sob1tie polucheni9 urona
            EventManager.takeDamage?.Invoke(PlayerController.GetHealthOfPlayer(), 1);
        }
    }

    // podpisatbs9 na sob1tie polucheni9 urona
    private void Start() { EventManager.takeDamage += onTakeDamage; }

    // otpisatbs9 ot sob1ti9 polucheni9 urona
    private void OnDestroy() { EventManager.takeDamage -= onTakeDamage; }

    //pri vb1zove sob1ti9 polucheni9 urona vb1poln9ets9 eta function
    private void onTakeDamage(Health health, uint damage)
    {
        health.Damage(damage);
    }
}
