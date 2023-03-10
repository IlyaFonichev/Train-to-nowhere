using UnityEngine;

public class StaticEventsOfPlayer : MonoBehaviour
{
    public delegate void TakeDamage(HealthOfPlayer health, uint damage);
    public static TakeDamage takeDamage;


    public delegate void Heal(HealthOfPlayer health);
    public static Heal heal;


    public delegate void ChangeHealthInterface(HealthOfPlayer health);
    public static ChangeHealthInterface changeHealthInterface;


    public delegate void ChangeScoreInterface(Score score);
    public static ChangeScoreInterface changeScoreInterface;
}
