using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCollectedCoins : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        text.text = Score.GetScore().ToString();
    }
}
