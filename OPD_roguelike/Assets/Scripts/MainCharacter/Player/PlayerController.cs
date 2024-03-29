using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float switchRoomOffet;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float dashSpeed = 1f;
    [SerializeField] private AnimationCurve dashSpeedCurve;
    [SerializeField] private float dashTime = 0.5f;
    private GameObject weapon;

    private Rigidbody _rigidBody;
    private bool isDashing;
    private bool weaponEquiped = false;
    public UnStaticEventsOfPlayer unStaticEventsOfPlayer = new UnStaticEventsOfPlayer();
    private static HealthOfPlayer _healthOfPlayer;
    private static Score _scoreOfOlayer;
    public static PlayerController instance;
    public static HealthOfPlayer GetHealthOfPlayer() { return _healthOfPlayer; }
    public static Score GetScoreOfPlayer() { return _scoreOfOlayer; }

    private void Awake()
    {
        SetInstance();
        _healthOfPlayer = new HealthOfPlayer(health: 70, maxHaelthValue: 100);
        _scoreOfOlayer = new Score(0);

        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void SetInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical);

       
        if (!isDashing)
            _rigidBody.velocity = direction.normalized * speed;
        StartCoroutine(Dash(direction));
    }
    private void OnLevelWasLoaded(int level)
    {
        _rigidBody.transform.position = Vector3.zero;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Training") ||
           other.CompareTag("Cave") ||
           other.CompareTag("Forest") ||
           other.CompareTag("Wasteland") ||
           other.CompareTag("Laboratory"))
        {
            SceneManager.LoadScene(other.tag);
            _rigidBody.transform.position = Vector3.zero;
        }
        if (other.CompareTag("ExitDoor"))
        {
            SceneManager.LoadScene("TestLobby");
            _rigidBody.transform.position = Vector3.zero;
        }
        if (other.CompareTag("LeftDoor") ||
           other.CompareTag("RightDoor") ||
           other.CompareTag("TopDoor") ||
           other.CompareTag("BottomDoor"))
        {
            RoomSwitcher.instance.Switch(other.tag);
            GameObject currentRoom = RoomSwitcher.instance.CurrentRoom.GetComponent<Room>().OriginRoom;
            switch (other.tag)
            {
                case "LeftDoor":
                    for(int i = 0; i < currentRoom.transform.childCount; i++)
                        if(currentRoom.transform.GetChild(i).CompareTag("RightDoor"))
                        {
                            _rigidBody.transform.position = currentRoom.transform.GetChild(i).position + Vector3.left * switchRoomOffet;
                            break;
                        }
                    break;
                case "RightDoor":
                    for (int i = 0; i < currentRoom.transform.childCount; i++)
                        if (currentRoom.transform.GetChild(i).CompareTag("LeftDoor"))
                        {
                            _rigidBody.transform.position = currentRoom.transform.GetChild(i).position + Vector3.right * switchRoomOffet;
                            break;
                        }
                    break;
                case "TopDoor":
                    for (int i = 0; i < currentRoom.transform.childCount; i++)
                        if (currentRoom.transform.GetChild(i).CompareTag("BottomDoor"))
                        {
                            _rigidBody.transform.position = currentRoom.transform.GetChild(i).position + Vector3.forward * switchRoomOffet;
                            break;
                        }
                    break;
                case "BottomDoor":
                    for (int i = 0; i < currentRoom.transform.childCount; i++)
                        if (currentRoom.transform.GetChild(i).CompareTag("TopDoor"))
                        {
                            _rigidBody.transform.position = currentRoom.transform.GetChild(i).position + Vector3.back * switchRoomOffet;
                            break;
                        }
                    break;
                default:
                    break;
            }
        }
    }    
    public Rigidbody Rigidbody
    {
        get { return _rigidBody; }
    }    
}