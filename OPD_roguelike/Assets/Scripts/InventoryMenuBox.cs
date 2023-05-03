using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryMenuBox : MonoBehaviour, IPointerClickHandler
{
    protected GameObject item;

    public void printItem()
    {
        if (item != null)
        {
            Image img;
            if (item.TryGetComponent<Image>(out img))
                gameObject.GetComponent<Image>().sprite = img.sprite;
            else
                gameObject.GetComponent<Image>().sprite = item.GetComponentInChildren<SpriteRenderer>().sprite;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            SelectedItemPanel panel = SelectedItemPanel.instance;

            panel.ItemBox.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
            panel.ItemName.GetComponent<TMP_Text>().text = item.name;
            panel.ItemDescription.GetComponent<TMP_Text>().text = "";
        }
    }

    public GameObject Item
    {
        set { item = value; }
    }
}
