using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class chooseAbilityMenu : MonoBehaviour
{
    public static chooseAbilityMenu instance;

    private void Awake()
    {
        SetInstance();
        gameObject.SetActive(false);
    }

    private void SetInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        Image[] abilityBoxes = gameObject.GetComponentsInChildren<Image>();

        for (int i = 1; i < PassiveAbilitiesInventory.instance.gameObject.transform.childCount + 1; i++)
        {
            abilityBoxes[i].sprite = PassiveAbilitiesInventory.instance.gameObject.transform.GetChild(i - 1).gameObject.GetComponent<SpriteRenderer>().sprite;
            abilityBoxes[i].color = PassiveAbilitiesInventory.instance.gameObject.transform.GetChild(i - 1).gameObject.GetComponent<SpriteRenderer>().color;

            abilityBoxes[i].gameObject.GetComponent<AbilityMenuBox>().Ability = PassiveAbilitiesInventory.instance.gameObject.transform.GetChild(i - 1).gameObject;
        }
    }
}
