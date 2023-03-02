using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looting : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Score.AddScore(50);
        }
    }

}
