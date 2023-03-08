using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //хз, вроде надо
    private Collider _currentCollider;

    //при стоянии в огне
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //событие получения урона
            EventManager.takeDamage?.Invoke(PlayerController.GetHealthOfPlayer(), 1);
        }
    }

    // подписываемся на событие получения урона
    private void Start() { EventManager.takeDamage += onTakeDamage; }

    // отписываемся от события получения урона
    private void OnDestroy() { EventManager.takeDamage -= onTakeDamage; }

    //при вызове событи получения урона выполняется эта функция
    private void onTakeDamage(Health health, uint damage)
    {
        health.Damage(damage);
    }
}
