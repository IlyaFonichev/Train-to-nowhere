using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
    private CSVReader reader;
    private string[] data;

    private void Start()
    {
        reader = CSVReader.instance;
        data = new string[reader.ReadCSV().Length];
        reader.ReadCSV().CopyTo(data, 0);
    }

    private int FindNameParams()
    {
        string[] names = new string[data.Length / 8 - 1];

        for (int i = 8; i < data.Length - 1; i += 8)
            names[i / 8 - 1] = data[i];

        if (names.Contains(gameObject.name))
            return Array.IndexOf(names, gameObject.name);
        else
            return -1;
    }

    public float[] MakeParamsArray()
    {
        float[] parameters = new float[7];

        int index = FindNameParams();

        for (int i = (index + 1) * 8 + 1; i < (index + 1) * 8 + 8; i++)
        {
            parameters[i - ((index + 1) * 8 + 1)] = float.Parse(data[i]);
        }

        return parameters;
    }
}
