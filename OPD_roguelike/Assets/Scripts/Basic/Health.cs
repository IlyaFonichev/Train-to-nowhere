using UnityEngine;
using UnityEngine.SceneManagement;

public class Health
{
    //tecuLLLee zdorovbe, maximalnoe zdorovbe dl9 eczempl9ra
    [SerializeField] protected uint _healthValue;
    [SerializeField] protected uint _MaxHaelthValue;

    //constructor zdorovb9
    public Health(uint health, uint maxHaelthValue)
    {
        _healthValue = health;
        _MaxHaelthValue = maxHaelthValue;
    }

    //setter dl9 _healthValue
    public uint GetHealth() { return _healthValue; }

    //setter dl9 _MaxHaelthValue
    public uint GetMaxHaelth() { return _MaxHaelthValue; }

    //poluchenie urona
    public virtual void Damage(uint damageCalue) { _healthValue -= damageCalue; }

    //lechenie
    public virtual void Heal(uint healValue) { _healthValue += healValue;}

    //smertb
    public virtual void Kill() { _healthValue = 0; }
}
