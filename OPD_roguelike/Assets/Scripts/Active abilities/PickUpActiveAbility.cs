using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpActiveAbility : MonoBehaviour
{
    private GameObject curAbility;
    private GameObject player;

    private void Start()
    {
        player = PlayerController.instance.gameObject;
    }

    // realize later appearence of image in box
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            if ((gameObject.transform.position - player.transform.position).magnitude < 2)
            {
                GameObject curAbility = player.gameObject.GetComponent<InventoryScript>().ability;
                if (curAbility != null )
                {
                    GameObject spawnedCurAbility = Instantiate(curAbility, transform.position, Quaternion.Euler(90, 0, 0));
                    spawnedCurAbility.GetComponent<PickUpActiveAbility>().enabled = true;
                    spawnedCurAbility.GetComponent<SpriteRenderer>().enabled = true;
                    spawnedCurAbility.name = curAbility.name;

                    Destroy(curAbility);
                }

                GameObject newAbility = Instantiate(gameObject, player.transform);
                newAbility.GetComponent<PickUpActiveAbility>().enabled = false;
                newAbility.GetComponent<SpriteRenderer>().enabled = false;
                newAbility.name = gameObject.name;
                player.gameObject.GetComponent<InventoryScript>().ability = newAbility;

                CanvasInstance.instance.activeAbilityBox.GetComponent<Image>().sprite = newAbility.GetComponent<SpriteRenderer>().sprite;

                Destroy(gameObject);
            }
    }
}
