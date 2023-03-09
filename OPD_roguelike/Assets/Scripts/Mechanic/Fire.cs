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
}
