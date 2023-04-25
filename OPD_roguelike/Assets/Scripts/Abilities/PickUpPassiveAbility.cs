using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPassiveAbility : MonoBehaviour
{
    private GameObject player;
    private GameObject storedAbility;

    private void Start()
    {
        player = PlayerController.instance.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject inventory = PassiveAbilitiesInventory.instance.gameObject;

            if (inventory.transform.childCount >= 6)
                chooseAbilityMenu.instance.gameObject.SetActive(true);

            storedAbility = Instantiate(gameObject, inventory.transform);
            storedAbility.name = gameObject.name;
            storedAbility.GetComponent<SpriteRenderer>().enabled = false;
            storedAbility.GetComponent<BoxCollider>().enabled = false;
            storedAbility.GetComponent<PickUpPassiveAbility>().enabled = false;

            foreach (Ability ab in storedAbility.GetComponents<Ability>())
                ab.Accept(player.GetComponent<AbilityUser>());

            Destroy(gameObject);
        }
    }
}
