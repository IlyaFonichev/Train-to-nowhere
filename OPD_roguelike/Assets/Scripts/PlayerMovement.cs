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
    //[SerializeField] private float _dashCooldown = 1f;
    [SerializeField] private GameObject _scope;
    [SerializeField] private GameObject _shot;

    private Rigidbody _rb;
    private float deltaPosX, deltaPosY, deltaPosZ;
    private bool _isDashing;
    private RectTransform _scope_rt;
    private float canvasRadius;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _scope_rt =  _scope.GetComponent<RectTransform>();

        // Чтобы камеру потом можно было двигать
        deltaPosX = playerCamera.transform.position.x - transform.position.x;
        deltaPosY = playerCamera.transform.position.y - transform.position.y;
        deltaPosZ = playerCamera.transform.position.z - transform.position.z;

        // Для стрельбы (перспективная камера под углом)
        canvasRadius = Mathf.Tan((playerCamera.fieldOfView / 2) * Mathf.Deg2Rad) * playerCamera.nearClipPlane;
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

        var mousePos2D = Input.mousePosition;
        var screenToCameraDistance = playerCamera.nearClipPlane;

        var mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, screenToCameraDistance);

        // искомая точка в мировых координатах
        var worldPointPos = playerCamera.ScreenToWorldPoint(mousePosNearClipPlane);

        //Debug.Log(worldPointPos.x + " " + worldPointPos.y + " " + worldPointPos.z);

        Vector3 mousevector = new Vector3(worldPointPos.x - transform.position.x, transform.position.y, playerCamera.transform.position.z - worldPointPos.z - transform.position.z);
        //mousevector = new vector3(mousevector.x, transform.position.y, mousevector.y);
        mousevector = mousevector - transform.position;

        Debug.Log(mousevector.x + " " + mousevector.y + " " + mousevector.z);

        mousevector.Normalize();

        GameObject bullet = Instantiate(_shot, transform.position + (mousevector) * 2, new Quaternion(0,0,0,0));

        float elapsedtime = 0f;
        while (elapsedtime < 1)
        {
            bullet.transform.Translate(mousevector * speed * Time.fixedDeltaTime * _dashSpeed);

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

        // траблы с перемещением после дэша
        //elapsedTime = 0f;
        //while (elapsedTime < _dashCooldown)
        //{
        //    elapsedTime += Time.deltaTime;
        //    yield return new WaitForSeconds(Time.deltaTime);
        //}

        _isDashing = false;
        yield break;
    }

    private void showScope()
    {
        Vector2 mousePos = Input.mousePosition;
        _scope_rt.anchoredPosition = mousePos;
    }
}