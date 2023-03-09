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

    //pereopredel9em metodb1 iz klassa Health dl9 gero9
    public override void Damage(uint damageCalue)
    {
        _healthValue -= damageCalue;
        if (_healthValue <= 0)
            SceneManager.LoadScene("EndScene");
        //обновляется интерфейс здоровья для героя
        EventManager.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }

    public override void Heal(uint healValue)
    {
        _healthValue += healValue;
        //обновляется интерфейс здоровья для героя
        EventManager.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }

    public override void Kill()
    {
        _healthValue = 0;
        EventManager.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }
}
