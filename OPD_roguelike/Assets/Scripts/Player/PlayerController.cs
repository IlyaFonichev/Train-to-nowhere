using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float dashSpeed = 1f;
    [SerializeField] private AnimationCurve dashSpeedCurve;
    [SerializeField] private float dashTime = 0.5f;
    [SerializeField] private GameObject weapon;

    private Rigidbody _rigidBody;
    private bool isDashing;
    private bool weaponEquiped = false;

    public UnStaticEventsOfPlayer unStaticEventsOfPlayer = new UnStaticEventsOfPlayer();

    private static HealthOfPlayer _healthOfPlayer;
    private static Score _scoreOfOlayer;

    public static HealthOfPlayer GetHealthOfPlayer() { return _healthOfPlayer; }
    public static Score GetScoreOfPlayer() { return _scoreOfOlayer; }

    private void Start()
    {
        _healthOfPlayer = new HealthOfPlayer(health: 70, maxHaelthValue: 100);
        _scoreOfOlayer = new Score(0);

        _rigidBody = gameObject.GetComponent<Rigidbody>();       
    }


    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical);

        move(direction);
        StartCoroutine(Dash(direction));      
    }

    private void Update()
    {
        instantiateWeapon();
    }

    private void instantiateWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponEquiped = !weaponEquiped;
            weapon.SetActive(weaponEquiped);
        }
    }

    private void move(Vector3 direction)
    {
        if (isDashing) return;
        _rigidBody.velocity = direction.normalized * speed;
    }

    private IEnumerator Dash(Vector3 direction)
    {
        if (Input.GetAxisRaw("Roll") == 0) yield break;
        if (direction == Vector3.zero) yield break;
        if (isDashing) yield break;

        isDashing = true;

        float elapsedTime = 0.4f;
        while (elapsedTime < dashTime)
        {
            _rigidBody.velocity = direction.normalized * speed * Time.fixedDeltaTime * dashSpeed * 100;

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        isDashing = false;
        yield break;
    }

    public bool getDash()
    {
        return isDashing;
    }
}