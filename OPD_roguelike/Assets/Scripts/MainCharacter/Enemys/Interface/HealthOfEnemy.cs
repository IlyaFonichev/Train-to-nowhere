public class HealthOfEnemy : Health
{
    private EnemyController enemyController;

    public void SetEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    public HealthOfEnemy(uint health, uint maxHaelthValue) : base(health, maxHaelthValue)
    {
        _healthValue = health;
        _MaxHaelthValue = maxHaelthValue;
    }

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
