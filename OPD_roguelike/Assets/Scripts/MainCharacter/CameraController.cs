using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject scope;
    private GameObject player;
    [SerializeField] private float smoothSpeed = 0.125f;

    private RectTransform scope_rt;
    private float deltaPosX, deltaPosY, deltaPosZ;

    private void Start()
    {
        player = PlayerController.instance.gameObject;
        scope_rt = scope.GetComponent<RectTransform>();

        // „тобы камеру потом можно было двигать
        deltaPosX = transform.position.x - player.transform.position.x;
        deltaPosY = transform.position.y - player.transform.position.y;
        deltaPosZ = transform.position.z - player.transform.position.z;

    }

    private void FixedUpdate()
    {
        showScope();
        followPlayer();
    }

    private void showScope()
    {
        Cursor.visible = false;
        Vector2 mousePos = Input.mousePosition;
        scope_rt.anchoredPosition = mousePos;
    }

    private void followPlayer()
    {
        Vector3 target = player.transform.position + new Vector3(deltaPosX, deltaPosY, deltaPosZ);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
