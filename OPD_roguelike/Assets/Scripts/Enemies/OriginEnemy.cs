using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private Rigidbody rigidbody;
    private Condition condition;
    private Vector3 roomOrigin, targetVector;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 direction;
    private bool selecting;

    public enum Condition
    {
        None,
        Idle,
        Angry,
        Die
    }
    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        selecting = false;
        condition = Condition.Idle;
        StartCoroutine(SelectTargetPosition());
        if (RoomSwitcher.instance != null)
            roomOrigin = RoomSwitcher.instance.CurrentRoom.transform.position;
        else
            roomOrigin = Vector3.zero;
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move(rigidbody, targetVector, selecting, condition, direction, speed);
    }
    public abstract void Move(Rigidbody rigidbody, Vector3 targetVector, bool selecting, Condition condition, Vector3 direction, float speed);

    public IEnumerator SelectTargetPosition()
    {
        selecting = true;
        direction = Vector3.zero;
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        targetVector = new Vector3(roomOrigin.x + Random.Range(-6f, 6f), 0, roomOrigin.z + Random.Range(-3f, 3f));
        direction = (targetVector - rigidbody.transform.position).normalized;
        selecting = false;
    }

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

    private void TakeDamge()
    {
        if (currentHealth > 0)
        {
            currentHealth -= 1;
            UpdateHealthBar();
        }
        if (currentHealth == 0)
            StartCoroutine(Death());
    }
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
    IEnumerator Death()
    {
        condition = Condition.Die;
        Destroy(canvas);
        Destroy(spriteManager);
        for(int i = 0; i < particleSystemManager.transform.childCount; i++)
        {
            particleSystemManager.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        }
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
