using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class OriginEnemy : MonoBehaviour
{
    public float attackTime;
    [SerializeField]
    private GameObject spriteManager, canvas, particleSystemManager;
    [SerializeField]
    private Image healthBar;
    [SerializeField, Min(3)]
    private int maxHealth;
    private int currentHealth;
    [HideInInspector]
    public Rigidbody rb;
    private Condition _condition;
    [HideInInspector]
    public Vector3 roomOrigin;
    [SerializeField]
    private float speed, damage;
    [HideInInspector]
    public GameObject damageZone;

    public enum Condition
    {
        Spawn,
        Walking,
        Died
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        _condition = Condition.Spawn;

        if (SceneManager.GetActiveScene().name == "CaveTest") //Test
            Spawn();
    }

    private void FixedUpdate()
    {
        if (_condition == Condition.Walking)
            Move();
    }

    public abstract void Initialization();

    public abstract IEnumerator Attack();

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
        //Test
        if (_condition != Condition.Died)
            StartCoroutine(Death());
    }

    private void OnDestroy()
    {
        if (PlayerController.instance != null && PlayerController.instance.gameObject.GetComponent<InventoryScript>().ability != null)
            PlayerController.instance.gameObject.GetComponent<InventoryScript>().ability.GetComponent<ActiveAbility>().curCoolDown -= 1;
    }

    public void Spawn()
    {
        _condition = Condition.Walking;
        SetRoomPosition();
        StartCoroutine(Attack());
        Initialization();
    }

    private void SetRoomPosition()
    {
        if (RoomSwitcher.instance != null)
            roomOrigin = RoomSwitcher.instance.CurrentRoom.transform.position;
        else
            roomOrigin = Vector3.zero;
    }

    private void TakeDamge()
    {
        if (currentHealth > 0)
        {
            currentHealth -= 1;
            UpdateHealthBar();
        }
        if (currentHealth == 0 && _condition != Condition.Died)
            StartCoroutine(Death());
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    private IEnumerator Death()
    {
        _condition = Condition.Died;
        Destroy(canvas);
        Destroy(spriteManager);
        if (particleSystemManager != null)
            for (int i = 0; i < particleSystemManager.transform.childCount; i++)
                particleSystemManager.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        if (damageZone != null)
            Destroy(damageZone);
        MobsManager.instance.RemoveMob(gameObject);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public Condition condition
    {
        get { return _condition; }
    }
}
