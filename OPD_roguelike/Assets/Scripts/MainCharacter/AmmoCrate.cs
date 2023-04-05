using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    private WeaponScript ws;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ws = other.gameObject.GetComponent<InventoryScript>().firstWeapon.GetComponent<WeaponScript>();
            
            if (ws.currAmmo != ws.getTotalAmmo())
            {
                ws.currAmmo = ws.getTotalAmmo();
                ws.printAmmo();
                Destroy(gameObject);
            }
        }
    }
}
