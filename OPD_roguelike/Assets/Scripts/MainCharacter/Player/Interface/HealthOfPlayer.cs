using UnityEngine.SceneManagement;

public class HealthOfPlayer : Health
{
    public HealthOfPlayer(uint health, uint maxHaelthValue) : base(health, maxHaelthValue)
    {
        _healthValue = health;
        _MaxHaelthValue = maxHaelthValue;
    }

    public override void Damage(uint damageCalue)
    {
        _healthValue -= damageCalue;
        if (_healthValue <= 0)
            SceneManager.LoadScene("EndScene");
        StaticEventsOfPlayer.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }

    public override void Heal(uint healValue)
    {
        _healthValue += healValue;
        if (_healthValue > _MaxHaelthValue)
            _healthValue = _MaxHaelthValue;
        StaticEventsOfPlayer.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }

    public override void Kill()
    {
        _healthValue = 0;
        StaticEventsOfPlayer.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }

}
