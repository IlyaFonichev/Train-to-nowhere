using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class OriginEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject spriteManager, canvas, particleSystemManager, currentTerget, preTarget, nextTarget;
    [SerializeField]
    private Image healthBar;
    [SerializeField, Min(3)]
    private int maxHealth;
    private int currentHealth;
    [HideInInspector]
    public Rigidbody rigidbody;
    private Condition condition;
    private Vector3 roomOrigin, targetVector, nextTargetPosition, preTargetPosition;
    [SerializeField]
    private float speed;

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
        condition = Condition.Idle;
        if (RoomSwitcher.instance != null)
            roomOrigin = RoomSwitcher.instance.CurrentRoom.transform.position;
        else
            roomOrigin = Vector3.zero;
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody>();
        nextTargetPosition = new Vector3(roomOrigin.x + Random.Range(-6f, 6f), 0, roomOrigin.z + Random.Range(-3f, 3f));
        preTargetPosition = new Vector3(roomOrigin.x + Random.Range(-6f, 6f), 0, roomOrigin.z + Random.Range(-3f, 3f));
        targetVector = preTargetPosition;
    }

    private void FixedUpdate()
    {
        currentTerget.transform.position = targetVector;
        preTarget.transform.position = preTargetPosition;
        nextTarget.transform.position = nextTargetPosition;
        if (condition == Condition.Idle)
            Move();
    }
    public abstract void Move();

    public void Step()
    {
        speed = 3;
        preTargetPosition = nextTargetPosition;
        targetVector = preTargetPosition;
        while(Vector3.Distance(preTargetPosition, nextTargetPosition) < 5f)
            nextTargetPosition = new Vector3(roomOrigin.x + Random.Range(-6f, 6f), 0, roomOrigin.z + Random.Range(-3f, 3f));
    }
    public void CheckDistance()
    {
        targetVector = Vector3.MoveTowards(targetVector, nextTargetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(targetVector, nextTargetPosition) < Vector3.Distance(preTargetPosition, nextTargetPosition) / 2)
        {
            Step();
        }
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
    public float Speed
    {
        get { return speed; }
    }
    public Vector3 TargetPosition
    {
        get { return targetVector; }
    }
}
