using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject weapon;

    private float timer = 0;
    private float bulletLife = 0;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        bulletLife = weapon.GetComponent<WeaponScript>().getBulletLifeSeconds();
    }

    private void Update()
    {
        if (timer > bulletLife)
        {
            Destroy(gameObject);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
