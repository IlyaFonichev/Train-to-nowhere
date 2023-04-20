using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CSVReader : MonoBehaviour
{
    [SerializeField] private TextAsset textAssetData;

    public static CSVReader instance;

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public string[] ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] {";", "\n"}, StringSplitOptions.None);

        return data;
    }
}
