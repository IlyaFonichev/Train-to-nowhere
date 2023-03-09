using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthOfPlayer : Health
{
    //pereopredel9em konstructor
    public HealthOfPlayer(uint health, uint maxHaelthValue) : base(health, maxHaelthValue)
    {
        _healthValue = health;
        _MaxHaelthValue = maxHaelthValue;
    }


    //pereopredel9em metodb1 iz klassa Health dl9 enemy
    public override void Damage(uint damageCalue)
    {
        _healthValue -= damageCalue;
        if (_healthValue <= 0)
            SceneManager.LoadScene("EndScene");
        //Obnovl9ets9 interface zdorovb9 dl9 gero9
        EventManager.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }

    public override void Heal(uint healValue)
    {
        _healthValue += healValue;
        //Obnovl9ets9 interface zdorovb9 dl9 gero9
        EventManager.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }

    public override void Kill()
    {
        _healthValue = 0;
        //Obnovl9ets9 interface zdorovb9 dl9 gero9
        EventManager.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }

}
