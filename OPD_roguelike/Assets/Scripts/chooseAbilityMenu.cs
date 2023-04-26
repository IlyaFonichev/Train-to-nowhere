using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Text[] abilityNames = gameObject.GetComponentsInChildren<Text>();

        GameObject abilitiesInventory = PassiveAbilitiesInventory.instance.gameObject;
        for (int i = 1; i < abilitiesInventory.transform.childCount + 1; i++)
        {
            GameObject ability = abilitiesInventory.transform.GetChild(i - 1).gameObject;

            abilityBoxes[i].sprite = ability.GetComponent<SpriteRenderer>().sprite;
            abilityBoxes[i].color = ability.GetComponent<SpriteRenderer>().color;

            abilityNames[i - 1].text = ability.name;

            abilityBoxes[i].gameObject.GetComponent<AbilityMenuBox>().Ability = ability;
        }

        PauseManager.instance.gameObject.GetComponent<PauseManager>().setPause();
    }
}
