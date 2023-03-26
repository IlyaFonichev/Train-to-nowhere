using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] TextAsset ItemProperties;

    public TextAsset getItemProperties()
    {
        return ItemProperties;
    }

    public void setItemProperties(TextAsset ta)
    {
        ItemProperties = ta;
    }
}
