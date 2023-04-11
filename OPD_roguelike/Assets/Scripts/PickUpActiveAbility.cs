using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpActiveAbility : MonoBehaviour
{
    private GameObject curAbility;
    private GameObject player;

    private void Start()
    {
        player = PlayerController.instance.gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            if ((gameObject.transform.position - player.transform.position).magnitude < 2)
            {
                try
                {
                    curAbility = player.gameObject.GetComponent<InventoryScript>().ability;
                    curAbility.GetComponent<PickUpActiveAbility>().enabled = true;
                    curAbility.transform.position = player.transform.position;
                }
                catch { }

                player.gameObject.GetComponent<InventoryScript>().ability = gameObject;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<PickUpActiveAbility>().enabled = false;
            }
    }
}
