using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBoss : OriginEnemy
{
    [SerializeField]
    private ParticleSystem stickParticleSystem;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject bulletPrefab, meteorPrefab;
    private int bulletCount;
    private TypeOfAtack type;
    private Vector3 targetDashVector;
    private float maxSpeed;
    private Vector3 direction = Vector3.zero;
    [SerializeField]
    private GameObject hpBar;
    private enum TypeOfAtack
    {
        Stay,
        Dash,
        Fireball,
        MeteorShower
    }

    public override IEnumerator Attack()
    {
        Vector3 instantiatePosition = Vector3.zero;
        if (RoomSwitcher.instance != null)
            instantiatePosition = RoomSwitcher.instance.CurrentRoom.transform.position;
        while (!isSpawn)
        {
            yield return new WaitForSeconds(1);
        }
        while (condition != Condition.Died)
        {
            if (type == TypeOfAtack.Stay)
            {
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
                        direction = (targetDashVector - transform.position).normalized;
                        type = TypeOfAtack.Dash;
                        animator.SetInteger("Attack", 2);
                        break;
                    case 1:
                        type = TypeOfAtack.Fireball;
                        animator.SetInteger("Attack", 1);
                        bulletCount = Random.Range(9, 16);
                        float angle = Random.Range(1f, 360f / bulletCount);
                        for (int i = 0; i < bulletCount; i++)
                        {
                            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0, -0.5f), Quaternion.Euler(0, angle, 0));
                            angle += 360 / bulletCount;
                        }
                        yield return new WaitForSeconds(attackTime);
                        type = TypeOfAtack.Stay;
                        break;
                    case 2:
                        type = TypeOfAtack.MeteorShower;
                        animator.SetInteger("Attack", 3);
                        for (int i = 0; i < bulletCount; i++)
                        {
                            Instantiate(meteorPrefab, instantiatePosition + new Vector3(Random.Range(-14f, 14f), 0, Random.Range(-7f, 7f)), Quaternion.identity);
                            yield return new WaitForSeconds(0.5f);
                        }
                        yield return new WaitForSeconds(attackTime);
                        type = TypeOfAtack.Stay;
                        break;
                    default:
                        break;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }


    private void Update()
    {
        if(isSpawn)
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
                }
                else
                {
                    if (Speed > 0)
                        Speed -= Time.deltaTime * 50f;
                    else
                    {
                        rb.velocity = Vector3.zero;
                        Speed = 0;
                        type = TypeOfAtack.Stay;
                    }
                }
            }
        }
    }

    public override void Initialization()
    {
        hpBar.SetActive(false);
        isSpawn = false;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("isSpawn", true);
        yield return new WaitForSeconds(2f);
        stickParticleSystem.Play();
        hpBar.SetActive(true);
        yield return new WaitForSeconds(1f);
        isSpawn = true;
    }

    public override void Move()
    {

    }
}
