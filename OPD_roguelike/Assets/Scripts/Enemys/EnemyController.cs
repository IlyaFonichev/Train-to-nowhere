using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed = 2.5f;

    private void FixedUpdate()
    {
        // Вычисляем направление движения
        Vector3 direction = (_player.transform.position - transform.position).normalized;

        // Двигаем объект в направлении игрока с учетом скорости
        transform.position += direction * _speed * Time.deltaTime;
    }

    public EventManagerOfEnemy eme = new EventManagerOfEnemy();


    private HealthOfEnemy _HealthOfEnemy;
    
    public HealthOfEnemy GetHealthOfEnemy() { return _HealthOfEnemy; }


    private void Awake()
    {
        _HealthOfEnemy = new HealthOfEnemy(health: 1000, maxHaelthValue: 1000);
        _HealthOfEnemy.SetEnemyController(this);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            eme.EventTakeDamage(_HealthOfEnemy, 5);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.takeDamage?.Invoke(PlayerController.GetHealthOfPlayer(), 1);
        }
    }







    private void Start()
    {
        eme.takeDamage += onTakeDamage;
        eme.deathOfMob += onDeathOfMob;
        //EventManager.takeDamage += onPlayerTakeDamage;
    }

    private void onPlayerTakeDamage(HealthOfPlayer health, uint damage)
    {
        health.Damage(damage);
    }

    //
    private void OnDestroy() 
    {
        eme.takeDamage -= onTakeDamage;
        eme.deathOfMob -= onDeathOfMob;
        //EventManager.takeDamage -= onPlayerTakeDamage;
    }

    //
    private void onTakeDamage(HealthOfEnemy health, uint damage)
    {
        health.Damage(damage);
    }

    private void onDeathOfMob()
    {
        Destroy(gameObject);
    }
}
