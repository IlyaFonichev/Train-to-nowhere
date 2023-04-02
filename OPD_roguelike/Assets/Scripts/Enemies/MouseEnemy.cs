using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static OriginEnemy;

public class MouseEnemy : OriginEnemy
{
    [SerializeField]
    private GameObject knife;

    public override void Move(Rigidbody rigidbody, Vector3 targetVector, bool selecting, Condition condition, Vector3 direction, float speed)
    {
        if (Vector3.Distance(targetVector, rigidbody.transform.position) < 0.5f && !selecting)
        {
            StartCoroutine(SelectTargetPosition());
        }
        if (condition == Condition.Idle)
        {
            rigidbody.velocity = new Vector3(direction.x,
                0, direction.z) * speed;
        }
    }
}
