using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public static InventoryMenu instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (!PauseManager.instance.gameObject.GetComponent<PauseManager>().onPause)
            PauseManager.instance.gameObject.GetComponent<PauseManager>().setPause();

        InventoryMenuBox[] boxes = gameObject.GetComponentsInChildren<InventoryMenuBox>();

        boxes[0].Item = PlayerController.instance.gameObject.GetComponent<InventoryScript>().firstWeapon;
        boxes[1].Item = PlayerController.instance.gameObject.GetComponent<InventoryScript>().ability;

        for (int i = 0; i < PassiveAbilitiesInventory.instance.transform.childCount; i++)
        {
            boxes[i + 2].Item = PassiveAbilitiesInventory.instance.transform.GetChild(i).gameObject;
        }

        foreach (InventoryMenuBox box in boxes)
            box.printItem();
    }
}
