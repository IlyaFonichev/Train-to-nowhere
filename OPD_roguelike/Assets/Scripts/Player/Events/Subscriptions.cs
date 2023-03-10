using UnityEngine;

public class Subscriptions : MonoBehaviour
{
    [SerializeField] private PlayerController player;


    private void Start() 
    { 
        StaticEventsOfPlayer.takeDamage += onTakeDamage;
        player.useop.addScore += onAddScore;
        StaticEventsOfPlayer.heal += onHeal;
    }

    private void OnDestroy() 
    { 
        StaticEventsOfPlayer.takeDamage -= onTakeDamage;
        player.useop.addScore -= onAddScore;
        StaticEventsOfPlayer.heal -= onHeal;
    }


    //1
    private void onTakeDamage(HealthOfPlayer health, uint damage)
    {
        health.Damage(damage);
    }


    //2
    private void onAddScore(uint scoreValue)
    {
        PlayerController.GetScoreOfPlayer().AddScore(scoreValue);
    }

    //3
    private void onHeal(HealthOfPlayer health)
    {
        health.Heal(health.GetMaxHaelth());
    }
}
