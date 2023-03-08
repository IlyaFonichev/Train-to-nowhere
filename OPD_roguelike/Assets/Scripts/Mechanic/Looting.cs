using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looting : MonoBehaviour
{
    //событи не статическое, нужен элемент класса
    EventManager em = new EventManager();

    //если тригера коснулс€ игрок, то объект удал€етс€ и начисл€ютс€ очки
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            em.addScore?.Invoke(PlayerController.GetScoreOfPlayer(), 50);
        }
    }

    // подписываемс€ на событие добавлени€ очков
    private void Start()
    {
        em.addScore += onAddScore;
    }

    // отписываемс€ от событи€ добавлени€ очков
    private void OnDestroy()
    {
        em.addScore += onAddScore;
    }

    //при вызове событи€ добавлени€ очков выполн€етс€ эта функци€
    private void onAddScore(Score score, uint scoreValue)
    {
        score.AddScore(scoreValue);
    }
}
