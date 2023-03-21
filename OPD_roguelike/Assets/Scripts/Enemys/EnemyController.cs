using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed = 2.5f;
    private PlayerController player;

    public UnStaticEventsOfEnemy eme = new UnStaticEventsOfEnemy();

    private HealthOfEnemy _HealthOfEnemy;
    public HealthOfEnemy GetHealthOfEnemy() { return _HealthOfEnemy; }


    private void Awake()
    {
        player = PlayerController.instance;
        _HealthOfEnemy = new HealthOfEnemy(health: 1000, maxHaelthValue: 1000);
        _HealthOfEnemy.SetEnemyController(this);
    }

    private void Start()
    {
        eme.takeDamage += onTakeDamage;
        eme.deathOfMob += onDeathOfMob;
        //EventManager.takeDamage += onPlayerTakeDamage;
    }

    private void OnDestroy()
    {
        eme.takeDamage -= onTakeDamage;
        eme.deathOfMob -= onDeathOfMob;
        //EventManager.takeDamage -= onPlayerTakeDamage;
    }

    private void onTakeDamage(HealthOfEnemy health, uint damage)
    {
        health.Damage(damage);
    }

    private void onDeathOfMob()
    {
        player.unStaticEventsOfPlayer.EventAddScore(100);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;

        transform.position += direction * _speed * Time.deltaTime;
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
            StaticEventsOfPlayer.takeDamage?.Invoke(PlayerController.GetHealthOfPlayer(), 1);
        }
    }
}
