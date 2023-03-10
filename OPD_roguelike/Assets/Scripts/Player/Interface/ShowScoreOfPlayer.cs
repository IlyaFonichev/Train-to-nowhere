using UnityEngine;
using UnityEngine.UI;

public class ShowScoreOfPlayer : MonoBehaviour
{
    [SerializeField] private Text _scoreValueText;

    private void Start()
    {
        StaticEventsOfPlayer.changeScoreInterface += onChangeScoreInterface;
        StaticEventsOfPlayer.changeScoreInterface?.Invoke(PlayerController.GetScoreOfPlayer());
    }

    private void OnDestroy() 
    { 
        StaticEventsOfPlayer.changeScoreInterface -= onChangeScoreInterface; 
    }

    private void onChangeScoreInterface(Score score)
    {
        _scoreValueText.text = score.GetScore().ToString();
    }


}
