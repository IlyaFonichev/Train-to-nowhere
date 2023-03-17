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
        if (collision.collider.tag != "Bullet" && collision.collider.tag != "Player")
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
