using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthOfEnemy : Health
{

    private EnemyController enemyController;

    public void SetEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }


    //pereopredel9em konstructor
    public HealthOfEnemy(uint health, uint maxHaelthValue) : base(health, maxHaelthValue)
    {
        _healthValue = health;
        _MaxHaelthValue = maxHaelthValue;
    }

    //pereopredel9em metodb1 iz klassa Health dl9 gero9
    public override void Damage(uint damageCalue)
    {
        _healthValue -= damageCalue;
        if (_healthValue <= 0)
        {
            enemyController.eme.EventDeathOfMob();
        }
        enemyController.eme.EventChangeHealthInterface(this);
    }

    public override void Heal(uint healValue)
    {
        _healthValue += healValue;
        enemyController.eme.EventChangeHealthInterface(this);
    }

    public override void Kill()
    {
        _healthValue = 0;
        enemyController.eme.EventChangeHealthInterface(this);
    }
}
