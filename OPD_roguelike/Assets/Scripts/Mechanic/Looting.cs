using UnityEngine;

public class Looting : MonoBehaviour
{
    //сигнатура
    public delegate void AddScore(Score score, uint scoreValue);
    //событие: изменени€ интерфейса очков
    public AddScore addScore;

    //если тригера коснулс€ игрок, то объект удал€етс€ и начисл€ютс€ очки
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            addScore?.Invoke(PlayerController.GetScoreOfPlayer(), 50);
        }
    }

    // подписываемс€ на событие добавлени€ очков
    private void Start()
    {
        addScore += onAddScore;
    }

    // отписываемс€ от событи€ добавлени€ очков
    private void OnDestroy()
    {
        addScore += onAddScore;
    }

    //при вызове событи€ добавлени€ очков выполн€етс€ эта функци€
    private void onAddScore(Score score, uint scoreValue)
    {
        score.AddScore(scoreValue);
    }
}
