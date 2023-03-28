using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CSVReader : MonoBehaviour
{
    [SerializeField] private TextAsset textAssetData;

    public string[] ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] {";", "\n"}, StringSplitOptions.None);

        return data;
    }
}
