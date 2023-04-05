using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    private void Start()
    {
        GameObject chosenWeapon = weapons[Random.Range(0, weapons.Count)];
        GameObject weapon = Instantiate(chosenWeapon, transform.position, Quaternion.Euler(90, 0, 0));
        weapon.name = chosenWeapon.name;
        Destroy(gameObject);
    }
}
