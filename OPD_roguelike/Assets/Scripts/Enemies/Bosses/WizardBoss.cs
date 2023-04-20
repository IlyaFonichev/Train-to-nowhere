using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBoss : OriginEnemy
{
    [SerializeField]
    private GameObject bulletPrefab, meteorPrefab;
    private int bulletCount;
    private TypeOfAtack type;
    private Vector3 targetDashVector;
    private float maxSpeed;
    private Vector3 direction = Vector3.zero;
    private enum TypeOfAtack
    {
        Dash,
        Fireball,
        MeteorShower
    }

    public override IEnumerator Attack()
    {
        while (condition != Condition.Died)
        {
            Vector3 instantiatePosition = Vector3.zero;
            if (RoomSwitcher.instance != null)
                instantiatePosition = RoomSwitcher.instance.CurrentRoom.transform.position;
            switch (Random.Range(0, 3))
            {
                case 0:
                    Speed = 0;
                    maxSpeed = Random.Range(10f, 15f);
                    if (PlayerController.instance != null)
                        targetDashVector = PlayerController.instance.gameObject.transform.position;
                    else
                        do
                        {
                            targetDashVector = instantiatePosition + new Vector3(Random.Range(-14f, 14f), 0, Random.Range(-7f, 7f));
                        } while (Vector3.Distance(transform.position, targetDashVector) < 10f);
                    yield return new WaitForSeconds(1f);
                    type = TypeOfAtack.Dash;
                    break;
                case 1:
                    type = TypeOfAtack.Fireball;
                    bulletCount = Random.Range(9, 16);
                    float angle = Random.Range(1f, 360f / bulletCount);
                    for (int i = 0; i < bulletCount; i++)
                    {
                        Instantiate(bulletPrefab, transform.position + new Vector3(0, 0, -0.5f), Quaternion.Euler(0, angle, 0));
                        angle += 360 / bulletCount;
                    }
                    break;
                case 2:
                    for (int i = 0; i < bulletCount; i++)
                    {
                        Instantiate(meteorPrefab, instantiatePosition + new Vector3(Random.Range(-14f, 14f), 0, Random.Range(-7f, 7f)), Quaternion.identity);
                        yield return new WaitForSeconds(0.5f);
                    }
                    type = TypeOfAtack.MeteorShower;
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(attackTime);
        }
    }


    private void Update()
    {
        rb.velocity = new Vector3(direction.x,
            0, direction.z) * Speed;
        if (type == TypeOfAtack.Dash)
        {
            if (Vector3.Distance(transform.position, targetDashVector) > 0.5f)
            {
                if (Speed < maxSpeed)
                    Speed += Time.deltaTime * 25f;
                else
                    Speed = maxSpeed;
                direction = (targetDashVector - transform.position).normalized;
            }
            else
            {
                if (Speed > 0)
                    Speed -= Time.deltaTime * 50f;
                else
                    Speed = 0;
            }
        }
    }

    public override void Initialization()
    {

    }

    public override void Move()
    {

    }
}
