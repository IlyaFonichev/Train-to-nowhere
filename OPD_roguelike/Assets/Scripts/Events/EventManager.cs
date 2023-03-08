using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventManager : MonoBehaviour 
{
    //сигнатура
    public delegate void TakeDamage(Health health, uint damage);
    //событие: получения урона героем
    public static TakeDamage takeDamage;

    //мигнатура
    public delegate void ChangeHealthInterface(Health health);
    //событие: изменения интерфейса здоровья
    public static ChangeHealthInterface changeHealthInterface;

    //сигнатура
    public delegate void ChangeScoreInterface(Score score);
    //событие: изменения интерфейса очков
    public static ChangeScoreInterface changeScoreInterface;

    //сигнатура
    public delegate void AddScore(Score score, uint scoreValue);
    //событие: изменения интерфейса очков
    public AddScore addScore;

}

