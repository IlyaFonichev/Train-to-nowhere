using UnityEngine;
using UnityEngine.SceneManagement;

public class Health
{
    //текущее здоровье, масимальное здоровье для экземпляра
    [SerializeField] private uint _healthValue;
    [SerializeField] private uint _MaxHaelthValue;

    //конструктор здоровья
    public Health(uint health, uint maxHaelthValue)
    {
        _healthValue = health;
        _MaxHaelthValue = maxHaelthValue;
    }

    //setter для _healthValue
    public uint GetHealth() { return _healthValue; }

    //setter для _MaxHaelthValue
    public uint GetMaxHaelth() { return _MaxHaelthValue; }

    //при получении урона минусуется здоровье, проверяется не умер ли объект
    public void Damage(uint damageCalue) 
    { 
        _healthValue -= damageCalue;
        if (_healthValue <= 0)
            SceneManager.LoadScene("EndScene");
        //обновляется интерфейс здоровья для героя
        EventManager.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }

    //добавления здоровья
    public void Heal(uint healValue) 
    { 
        _healthValue += healValue;
        //обновляется интерфейс здоровья для героя
        EventManager.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }

    //смэрть
    public void Kill() 
    { 
        _healthValue = 0;
        EventManager.changeHealthInterface?.Invoke(PlayerController.GetHealthOfPlayer());
    }
}
