using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScoreOfPlayer : MonoBehaviour
{
    [SerializeField] private Text _scoreValueText;

    // Podpisb1vaems9 na sobb1tie izmeneni9 interfaca of score
    private void Start()
    { 
        EventManager.changeScoreInterface += onChangeScoreInterface;
        EventManager.changeScoreInterface?.Invoke(PlayerController.GetScoreOfPlayer());
    }

    // Otpisb1vaems9 na sobb1tie izmeneni9 interfaca of score
    private void OnDestroy() { EventManager.changeScoreInterface -= onChangeScoreInterface; }

    // vb1zb1vaets9 pri sob1tii izmeneni9 interfaca of score
    private void onChangeScoreInterface(Score score)
    {
        _scoreValueText.text = score.GetScore().ToString();
    }


}
