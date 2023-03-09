using UnityEngine;

public class Looting : MonoBehaviour
{
    //Signatura
    public delegate void AddScore(Score score, uint scoreValue);
    //Sobb1tie: izmenenie interfaca ochcov
    public AddScore addScore;

    //Esli trigera cosnuls9 igrok, to ob'ect udal9ets9 i nachisl9uts9 ochki
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            addScore?.Invoke(PlayerController.GetScoreOfPlayer(), 50);
        }
    }

    // podpisatbs9 na sobb1tie dobavleni9 ochkov
    private void Start()
    {
        addScore += onAddScore;
    }

    // otpisatbs9 ot sobb1tie dobavleni9 ochkov
    private void OnDestroy()
    {
        addScore += onAddScore;
    }

    //pri vb1zove sobb1ti9 dobavleni9 ochkov vb1poln9ets9 eta function
    private void onAddScore(Score score, uint scoreValue)
    {
        score.AddScore(scoreValue);
    }
}
