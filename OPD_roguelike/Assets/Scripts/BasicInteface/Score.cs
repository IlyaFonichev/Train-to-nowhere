using UnityEngine;

public class Score
{
    //Ob'ect v convas. Otobrazaet schet
    private static uint _scoreValue;

    //Constructor. Startovoe colichestvo ochkov
    public Score(uint scoreValue) { _scoreValue = scoreValue; }

    //setter dl9 _scoreValue
    public uint GetScore() { return _scoreValue; }

    //dobavlenie ochkov
    public void AddScore(uint newScoreValue) 
    { 
        _scoreValue += newScoreValue;
        EventManager.changeScoreInterface?.Invoke(PlayerController.GetScoreOfPlayer());
    }

}
