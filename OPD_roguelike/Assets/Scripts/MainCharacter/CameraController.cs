using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject scope;
    private GameObject player;
    [SerializeField] private float smoothSpeed = 0.125f;

    private RectTransform scope_rt;
    private Vector3 deltaPosition;

    public static CameraController instance;

    private Vector3 currentRoomPosition;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SetInstance();
    }

    private void SetInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        player = PlayerController.instance.gameObject;

        if (scope != null)
            scope.TryGetComponent<RectTransform>(out scope_rt);

        // „тобы камеру потом можно было двигать
        deltaPosition.x = transform.position.x - player.transform.position.x;
        deltaPosition.y = transform.position.y - player.transform.position.y;
        deltaPosition.z = transform.position.z - player.transform.position.z;

    }

    private void FixedUpdate()
    {
        showScope();
        followPlayer();
    }

    private void showScope()
    {
        if (scope_rt)
        {
            Vector2 mousePos = Input.mousePosition;
            scope_rt.anchoredPosition = mousePos;
        }
    }

    private void followPlayer()
    {
        Vector3 target = player.transform.position + deltaPosition;
        transform.position = Vector3.Lerp(transform.position, target, smoothSpeed);
        if (RoomSwitcher.instance != null)
        {
            if (transform.position.z < currentRoomPosition.z - 4.5f)
                transform.position = new Vector3(transform.position.x, transform.position.y, currentRoomPosition.z - 4.5f);
            if (transform.position.z > currentRoomPosition.z - 3)
                transform.position = new Vector3(transform.position.x, transform.position.y, currentRoomPosition.z - 3);
            if (transform.position.x < currentRoomPosition.x - 1)
                transform.position = new Vector3(currentRoomPosition.x - 1, transform.position.y, transform.position.z);
            if (transform.position.x > currentRoomPosition.x + 1)
                transform.position = new Vector3(currentRoomPosition.x + 1, transform.position.y, transform.position.z);
        }
    }

    public void SetCurrentRoom()
    {
        if (RoomSwitcher.instance != null)
            currentRoomPosition = RoomSwitcher.instance.CurrentRoom.transform.position;
        else
            currentRoomPosition = Vector3.zero;
    }

    public Vector3 DeltaPosition
    {
        get { return deltaPosition; }
    }
}
