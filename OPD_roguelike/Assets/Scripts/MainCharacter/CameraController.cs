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

        // Чтобы камеру потом можно было двигать
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
        float offsetYdown = 0, offsetYup = 0, offsetX = 0;
        if (RoomSwitcher.instance != null)
        {
            switch (RoomSwitcher.instance.CurrentRoom.GetComponent<Room>().Size)
            {
                case 1:
                    offsetX = 1;
                    offsetYdown = -4.5f;
                    offsetYup = -3;
                    break;
                case 2:
                    offsetX = 9;
                    offsetYdown = -9f;
                    offsetYup = 1;
                    break;
                default:
                    Debug.Log("Размер комнаты не задан");
                    break;
            }
            if (transform.position.z < currentRoomPosition.z + offsetYdown)
                transform.position = new Vector3(transform.position.x, transform.position.y, currentRoomPosition.z + offsetYdown);
            if (transform.position.z > currentRoomPosition.z + offsetYup)
                transform.position = new Vector3(transform.position.x, transform.position.y, currentRoomPosition.z + offsetYup);
            if (transform.position.x < currentRoomPosition.x - offsetX)
                transform.position = new Vector3(currentRoomPosition.x - offsetX, transform.position.y, transform.position.z);
            if (transform.position.x > currentRoomPosition.x + offsetX)
                transform.position = new Vector3(currentRoomPosition.x + offsetX, transform.position.y, transform.position.z);
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
