using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Score
{
    private static uint _scoreValue;

    public Score(uint scoreValue) { _scoreValue = scoreValue; }

    public uint GetScore() { return _scoreValue; }

    public void AddScore(uint newScoreValue) 
    { 
        _scoreValue += newScoreValue;
        StaticEventsOfPlayer.changeScoreInterface?.Invoke(PlayerController.GetScoreOfPlayer());
    }

}
