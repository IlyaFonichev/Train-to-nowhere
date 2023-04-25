using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityMenuBox : MonoBehaviour, IPointerClickHandler
{
    private GameObject ability;

    public GameObject Ability
    {
        set { ability = value; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject storedAbility = Instantiate(ability, PlayerController.instance.gameObject.transform.position + new Vector3(1, 0, 1) * 2, Quaternion.Euler(90, 0, 0));

        storedAbility.name = ability.name;
        storedAbility.GetComponent<SpriteRenderer>().enabled = true;
        storedAbility.GetComponent<BoxCollider>().enabled = true;
        storedAbility.GetComponent<PickUpPassiveAbility>().enabled = true;

        ability.GetComponent<Ability>().DisableBuff();

        gameObject.transform.parent.gameObject.SetActive(false);

        Destroy(ability);
    }
}
