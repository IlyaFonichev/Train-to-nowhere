using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCollectedCoins : MonoBehaviour
{
    [SerializeField] private Text text;

    private void Start()
    {
        text.text = PlayerController.GetScoreOfPlayer().GetScore().ToString();
    }
}
