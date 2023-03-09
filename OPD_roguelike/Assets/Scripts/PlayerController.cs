using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EventManager;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float dashSpeed = 1f;
    [SerializeField] private AnimationCurve dashSpeedCurve;
    [SerializeField] private float dashTime = 0.5f;
    [SerializeField] private GameObject shot;
    
    private Rigidbody rb;
    private bool isDashing;
    private Vector3 canvasCenter;

    private static HealthOfPlayer _healthOfPlayer;
    private static Score _scoreOfOlayer;
    public static HealthOfPlayer GetHealthOfPlayer() { return _healthOfPlayer; }
    public static Score GetScoreOfPlayer() { return _scoreOfOlayer; }

    private void Start()
    {
        //создаем здоровье героя
        _healthOfPlayer = new HealthOfPlayer(health: 70, maxHaelthValue: 100);
        //загружаем интерфейс здоровья
        _healthOfPlayer = new HealthOfPlayer(health: 70, maxHaelthValue: 100);
        EventManager.changeHealthInterface?.Invoke(_healthOfPlayer);
        //пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ
        _scoreOfOlayer = new Score(0);
        //пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ
        EventManager.changeScoreInterface?.Invoke(_scoreOfOlayer);



        rb = gameObject.GetComponent<Rigidbody>();       

        // пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
        canvasCenter = new Vector3(Screen.width / 2, Screen.height / 2, playerCamera.nearClipPlane);
    }


    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical);

        move(direction);
        StartCoroutine(shoot());
        StartCoroutine(Dash(direction));      
    }

    private void move(Vector3 direction)
    {
        //Health.HitToPlayer(1);

        if (isDashing) return;
        rb.velocity = direction.normalized * speed;
    }

    private IEnumerator shoot()
    {
        if (Input.GetAxisRaw("Fire") == 0) yield break;
        if (isDashing) yield break;

        Vector3 mousePos2D = Input.mousePosition;

        Vector3 mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, playerCamera.nearClipPlane);
        
        Vector3 worldPointPos = playerCamera.ScreenToWorldPoint(mousePosNearClipPlane);
        Vector3 currCanvasCenter = playerCamera.ScreenToWorldPoint(canvasCenter);

        Vector3 mouseVector = worldPointPos - currCanvasCenter;
        mouseVector = new Vector3(mouseVector.x, 0, mouseVector.z);

        mouseVector.Normalize();


        GameObject bullet = Instantiate(shot, transform.position + (mouseVector) * 2, new Quaternion(0, 0, 0, 0));

        float elapsedtime = 0f;
        while (elapsedtime < 1)
        {
            bullet.transform.Translate(mouseVector * speed * Time.fixedDeltaTime * dashSpeed);

            elapsedtime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Destroy(bullet);
        

        yield break;
    }

    private IEnumerator Dash(Vector3 direction)
    {
        if (Input.GetAxisRaw("Roll") == 0) yield break;
        if (direction == Vector3.zero) yield break;
        if (isDashing) yield break;

        isDashing = true;

        float elapsedTime = 0f;
        while (elapsedTime < dashTime)
        {
            rb.velocity = direction.normalized * speed * Time.fixedDeltaTime * dashSpeed;

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        isDashing = false;
        yield break;
    }
}