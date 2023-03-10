public class UnStaticEventsOfEnemy
{
    public delegate void TakeDamage(HealthOfEnemy health, uint damage);
    public TakeDamage takeDamage;

    public void EventTakeDamage(HealthOfEnemy health, uint damage)
    {
        takeDamage?.Invoke(health, damage);
    }


    public delegate void ChangeHealthInterface(HealthOfEnemy health);
    public ChangeHealthInterface changeHealthInterface;

    public void EventChangeHealthInterface(HealthOfEnemy health)
    {
        changeHealthInterface?.Invoke(health);
    }


    public delegate void DeathOfMob();
    public DeathOfMob deathOfMob;

    public void EventDeathOfMob()
    {
        deathOfMob?.Invoke();
    }
}
