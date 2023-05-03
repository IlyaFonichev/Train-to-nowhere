using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItemPanel : MonoBehaviour
{
    public static SelectedItemPanel instance;

    [SerializeField] private GameObject itemBox;
    [SerializeField] private GameObject itemName;
    [SerializeField] private GameObject itemDescription;

    public GameObject ItemBox
    {
        get { return itemBox; }
    }

    public GameObject ItemName
    {
        get { return itemName; }
    }

    public GameObject ItemDescription
    {
        get { return itemDescription; }
    }

    private void Awake()
    {
        instance = this;
    }
}
