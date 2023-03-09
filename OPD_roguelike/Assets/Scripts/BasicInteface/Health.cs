using UnityEngine;
using UnityEngine.SceneManagement;

public class Health
{
    //текущее здоровье, масимальное здоровье дл€ экземпл€ра
    [SerializeField] protected uint _healthValue;
    [SerializeField] protected uint _MaxHaelthValue;

    //конструктор здоровь€
    public Health(uint health, uint maxHaelthValue)
    {
        _healthValue = health;
        _MaxHaelthValue = maxHaelthValue;
    }

    //setter дл€ _healthValue
    public uint GetHealth() { return _healthValue; }

    //setter дл€ _MaxHaelthValue
    public uint GetMaxHaelth() { return _MaxHaelthValue; }

    //получение урона
    public virtual void Damage(uint damageCalue) { _healthValue -= damageCalue; }

    //лечение
    public virtual void Heal(uint healValue) { _healthValue += healValue;}

    //смэрть
    public virtual void Kill() { _healthValue = 0; }
}
