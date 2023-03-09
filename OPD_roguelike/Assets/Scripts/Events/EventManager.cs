using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventManager : MonoBehaviour 
{
    //signatura
    public delegate void TakeDamage(HealthOfPlayer health, uint damage);
    //sobb1tie: poluchenie urona geroem
    public static TakeDamage takeDamage;

    //signatura
    public delegate void ChangeHealthInterface(HealthOfPlayer health);
    //sobb1tie: izmenenie interfaca zdorovb9
    public static ChangeHealthInterface changeHealthInterface;

    //signatura
    public delegate void ChangeScoreInterface(Score score);
    //sobb1tie: izmenenie interfaca ochkov
    public static ChangeScoreInterface changeScoreInterface;

}

