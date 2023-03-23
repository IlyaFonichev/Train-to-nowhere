public class UnStaticEventsOfPlayer
{
    public delegate void AddScore(uint scoreValue);
    public AddScore addScore;

    public void EventAddScore(uint scoreValue)
    {
        addScore?.Invoke(scoreValue);
    }
}
