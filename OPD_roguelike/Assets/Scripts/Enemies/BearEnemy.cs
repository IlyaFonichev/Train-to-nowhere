using System.Collections;
using UnityEngine;

public class BearEnemy : OriginEnemy
{
    [SerializeField]
    private GameObject bulletPrefab;
    private Vector3 nextTargetPosition, preTargetPosition, targetVector;
    [SerializeField]
    private float attackTime;
    private int bulletCount;

    public override void Initialization()
    {
        nextTargetPosition = new Vector3(roomOrigin.x + Random.Range(-6f, 6f), 0, roomOrigin.z + Random.Range(-3f, 3f));
        preTargetPosition = new Vector3(roomOrigin.x + Random.Range(-6f, 6f), 0, roomOrigin.z + Random.Range(-3f, 3f));
        targetVector = preTargetPosition;
    }

    public override void Move()
    {
        CheckDistance();
        Vector3 direction = (targetVector - rb.transform.position).normalized;
        rb.velocity = new Vector3(direction.x,
            0, direction.z) * Speed;
    }

    public void CheckDistance()
    {
        targetVector = Vector3.MoveTowards(targetVector, nextTargetPosition, Speed * Time.deltaTime);
        if (Vector3.Distance(targetVector, nextTargetPosition) < Vector3.Distance(preTargetPosition, nextTargetPosition) / 3f)
            Step();
    }

    public void Step()
    {
        preTargetPosition = nextTargetPosition;
        targetVector = preTargetPosition;
        while (Vector3.Distance(preTargetPosition, nextTargetPosition) < 5f)
            nextTargetPosition = new Vector3(roomOrigin.x + Random.Range(-6f, 6f), 0, roomOrigin.z + Random.Range(-3f, 3f));
    }

    public override IEnumerator Attack()
    {
        float tempSpeed = Speed;
        while (condition != Condition.Died)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            Speed = 0;
            if (condition != Condition.Died)
            {
                bulletCount = 12;
                float angle = Random.Range(1f, 360f / bulletCount);
                for (int i = 0; i < bulletCount; i++)
                {
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0, -0.5f), Quaternion.Euler(0, angle, 0));
                    angle += 360 / bulletCount;
                }
            }
            yield return new WaitForSeconds(attackTime);
            Speed = tempSpeed;
        }
    }
}
