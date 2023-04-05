using UnityEngine;

public class MouseEnemy : OriginEnemy
{
    [SerializeField]
    private GameObject knife;

    public override void Move()
    {
        CheckDistance();
        Vector3 direction = (TargetPosition - rigidbody.transform.position).normalized;
        rigidbody.velocity = new Vector3(direction.x,
            0, direction.z) * Speed;
    }
}
