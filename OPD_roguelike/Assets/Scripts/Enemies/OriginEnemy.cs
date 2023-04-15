using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class OriginEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject spriteManager, canvas, particleSystemManager;
    [SerializeField]
    private Image healthBar;
    [SerializeField, Min(3)]
    private int maxHealth;
    private int currentHealth;
    [HideInInspector]
    public Rigidbody rb;
    private Condition condition;
    [HideInInspector]
    public Vector3 roomOrigin, targetVector, nextTargetPosition, preTargetPosition;
    [SerializeField]
    private float speed;

    public enum Condition
    {
        Spawn,
        Walking,
        Attacking,
        Died
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        condition = Condition.Spawn;

        if (SceneManager.GetActiveScene().name == "CaveTest") //Test
            Spawn();
    }

    public abstract void Initialization();

    private void FixedUpdate()
    {
        if (condition == Condition.Walking)
            Move();
    }

    public abstract void Move();

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bullet":
                TakeDamge();
                Destroy(other);
                break;
            default:
                break;
        }
    }

    private void OnMouseDown()
    {
        TakeDamge();
    }

    private void OnDestroy()
    {
        PlayerController.instance.gameObject.GetComponent<InventoryScript>().ability.GetComponent<ActiveAbility>().curCoolDown -= 1;
    }

    public void Spawn()
    {
        condition = Condition.Walking;
        Initialization();
    }

    private void TakeDamge()
    {
        if (currentHealth > 0)
        {
            currentHealth -= 1;
            UpdateHealthBar();
        }
        if (currentHealth == 0 && condition != Condition.Died)
            StartCoroutine(Death());
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    IEnumerator Death()
    {
        condition = Condition.Died;
        Destroy(canvas);
        Destroy(spriteManager);
        for(int i = 0; i < particleSystemManager.transform.childCount; i++)
        {
            particleSystemManager.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        }
        MobsManager.instance.RemoveMob(gameObject);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public float Speed
    {
        get { return speed; }
    }
    public Vector3 TargetPosition
    {
        get { return targetVector; }
    }
}
