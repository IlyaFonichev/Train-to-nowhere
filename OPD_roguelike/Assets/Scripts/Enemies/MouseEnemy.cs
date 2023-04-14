using UnityEngine;
using static OriginEnemy;

public class MouseEnemy : OriginEnemy
{
    [SerializeField]
    private GameObject knife;

    public override void Move()
    {
        CheckDistance();
        Vector3 direction = (TargetPosition - rb.transform.position).normalized;
        rb.velocity = new Vector3(direction.x,
            0, direction.z) * Speed;
    }

    public void CheckDistance()
    {
        targetVector = Vector3.MoveTowards(targetVector, nextTargetPosition, Speed * Time.deltaTime);
        if (Vector3.Distance(targetVector, nextTargetPosition) < Vector3.Distance(preTargetPosition, nextTargetPosition) / 1.66f)
            Step();
    }

    public void Step()
    {
        preTargetPosition = nextTargetPosition;
        targetVector = preTargetPosition;
        while (Vector3.Distance(preTargetPosition, nextTargetPosition) < 5f)
            nextTargetPosition = new Vector3(roomOrigin.x + Random.Range(-6f, 6f), 0, roomOrigin.z + Random.Range(-3f, 3f));
    }

    public override void Initialization()
    {
        if (RoomSwitcher.instance != null)
            roomOrigin = RoomSwitcher.instance.CurrentRoom.transform.position;
        else
            roomOrigin = Vector3.zero;
        nextTargetPosition = new Vector3(roomOrigin.x + Random.Range(-6f, 6f), 0, roomOrigin.z + Random.Range(-3f, 3f));
        preTargetPosition = new Vector3(roomOrigin.x + Random.Range(-6f, 6f), 0, roomOrigin.z + Random.Range(-3f, 3f));
        targetVector = preTargetPosition;
    }
}
