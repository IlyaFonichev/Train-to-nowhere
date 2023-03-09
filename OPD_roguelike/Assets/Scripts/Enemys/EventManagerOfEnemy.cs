using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerOfEnemy
{
    //signatura
    public delegate void TakeDamage(HealthOfEnemy health, uint damage);
    //sobb1tie: poluchenie urona geroem
    public TakeDamage takeDamage;

    public void EventTakeDamage(HealthOfEnemy health, uint damage)
    {
        takeDamage?.Invoke(health, damage);
    }


    //signatura
    public delegate void ChangeHealthInterface(HealthOfEnemy health);
    //sobb1tie: izmenenie interfaca zdorovb9
    public ChangeHealthInterface changeHealthInterface;

    public void EventChangeHealthInterface(HealthOfEnemy health)
    {
        changeHealthInterface?.Invoke(health);
    }


    //signatura
    public delegate void DeathOfMob();
    //sobb1tie: izmenenie interfaca zdorovb9
    public DeathOfMob deathOfMob;

    public void EventDeathOfMob()
    {
        deathOfMob?.Invoke();
    }
}
