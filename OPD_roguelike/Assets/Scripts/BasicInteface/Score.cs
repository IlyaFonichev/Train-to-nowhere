using UnityEngine;

public class Score
{
    //объект в canvas. Отображает счет
    private static uint _scoreValue;

    //конструктор. Стартовое количество очков
    public Score(uint scoreValue) { _scoreValue = scoreValue; }

    //setter для _scoreValue
    public uint GetScore() { return _scoreValue; }

    //добавление очков
    public void AddScore(uint newScoreValue) 
    { 
        _scoreValue += newScoreValue;
        EventManager.changeScoreInterface?.Invoke(PlayerController.GetScoreOfPlayer());
    }

}
