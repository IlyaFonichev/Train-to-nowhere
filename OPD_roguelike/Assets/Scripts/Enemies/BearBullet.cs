using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private GameObject psManager;
    [SerializeField]
    private float speed;
    private bool die;

    private void Start()
    {
        die = false;
        StartCoroutine(Wait());
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, 0, transform.rotation.eulerAngles.normalized.y) * Time.deltaTime * speed);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void _Die()
    {
        speed = 0;
        sprite.SetActive(false);
        for (int i = 0; i < psManager.transform.childCount; i++)
            psManager.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        StartCoroutine(Die());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") && !other.CompareTag("Untagged") && !die)
        {
            die = true;
            StopAllCoroutines();
            _Die();
        }
    }
}
