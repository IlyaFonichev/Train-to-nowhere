using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject weapon;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet") && !other.CompareTag("Player") && !other.CompareTag("Item"))
            Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(WaitAndDied(weapon.GetComponent<WeaponScript>().getBulletLifeSeconds()));
    }

    private IEnumerator WaitAndDied(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
