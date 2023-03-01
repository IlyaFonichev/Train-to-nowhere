using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float _dashSpeed = 1f;
    [SerializeField] private AnimationCurve _dashSpeedCurve;
    [SerializeField] private float _dashTime = 0.5f;
    [SerializeField] private GameObject _scope;
    [SerializeField] private GameObject _shot;

    private Rigidbody _rb;
    private float deltaPosX, deltaPosY, deltaPosZ;
    private bool _isDashing;
    private RectTransform _scope_rt;
    private Vector3 canvasCenter;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _scope_rt =  _scope.GetComponent<RectTransform>();

        // Чтобы камеру потом можно было двигать
        deltaPosX = playerCamera.transform.position.x - transform.position.x;
        deltaPosY = playerCamera.transform.position.y - transform.position.y;
        deltaPosZ = playerCamera.transform.position.z - transform.position.z;

        // Для вычисления вектора направления стрельбы
        canvasCenter = new Vector3(Screen.width / 2, Screen.height / 2, playerCamera.nearClipPlane);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, moveVertical, 0);

        move(direction);
        StartCoroutine(shoot());
        StartCoroutine(Dash(direction));
        showScope();
    }

    private void move(Vector3 direction)
    {
        if (_isDashing) return;
        transform.Translate(direction * speed * Time.fixedDeltaTime);
        playerCamera.transform.position = new Vector3(transform.position.x + deltaPosX, transform.position.y + deltaPosY, transform.position.z + deltaPosZ);
    }

    private IEnumerator shoot()
    {
        if (Input.GetAxisRaw("Fire") == 0) yield break;
        if (_isDashing) yield break;

        Vector3 mousePos2D = Input.mousePosition;

        Vector3 mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, playerCamera.nearClipPlane);
        
        Vector3 worldPointPos = playerCamera.ScreenToWorldPoint(mousePosNearClipPlane);
        Vector3 currCanvasCenter = playerCamera.ScreenToWorldPoint(canvasCenter);

        Vector3 mouseVector = worldPointPos - currCanvasCenter;
        mouseVector = new Vector3(mouseVector.x, 0, mouseVector.z);

        mouseVector.Normalize();

        GameObject bullet = Instantiate(_shot, transform.position + (mouseVector) * 2, new Quaternion(0,0,0,0));

        float elapsedtime = 0f;
        while (elapsedtime < 1)
        {
            bullet.transform.Translate(mouseVector * speed * Time.fixedDeltaTime * _dashSpeed);

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
        if (_isDashing) yield break;

        _isDashing = true;

        float elapsedTime = 0f;
        while (elapsedTime < _dashTime)
        {
            transform.Translate(direction * speed * Time.fixedDeltaTime * _dashSpeed);
            playerCamera.transform.position = new Vector3(transform.position.x + deltaPosX, transform.position.y + deltaPosY, transform.position.z + deltaPosZ);

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        _isDashing = false;
        yield break;
    }

    private void showScope()
    {
        Vector2 mousePos = Input.mousePosition;
        _scope_rt.anchoredPosition = mousePos;
    }
}