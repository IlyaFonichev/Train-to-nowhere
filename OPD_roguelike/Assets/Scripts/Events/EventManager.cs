using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventManager : MonoBehaviour 
{
    //сигнатура
    public delegate void TakeDamage(HealthOfPlayer health, uint damage);
    //событие: получения урона героем
    public static TakeDamage takeDamage;

    //мигнатура
    public delegate void ChangeHealthInterface(HealthOfPlayer health);
    //событие: изменения интерфейса здоровья
    public static ChangeHealthInterface changeHealthInterface;

    //сигнатура
    public delegate void ChangeScoreInterface(Score score);
    //событие: изменения интерфейса очков
    public static ChangeScoreInterface changeScoreInterface;

}

