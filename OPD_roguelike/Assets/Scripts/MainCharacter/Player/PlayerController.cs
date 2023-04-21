using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool isInstantiate;
    private string sceneName, currentSceneName;
    private float switchRoomOffet = 3f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float dashSpeed = 1f;
    [SerializeField] private AnimationCurve dashSpeedCurve;
    [SerializeField] private float dashTime = 0.5f;

    private Rigidbody _rigidBody;
    private bool isDashing;
    public UnStaticEventsOfPlayer unStaticEventsOfPlayer = new UnStaticEventsOfPlayer();
    private static HealthOfPlayer _healthOfPlayer;
    private static Score _scoreOfOlayer;
    public static PlayerController instance;
    [SerializeField]
    private GameObject weaponManager;
    public static HealthOfPlayer GetHealthOfPlayer() { return _healthOfPlayer; }
    public static Score GetScoreOfPlayer() { return _scoreOfOlayer; }

    private void Awake()
    {
        isInstantiate = false;
        if (SceneManager.GetActiveScene().name == "TestLobby")
        {
            PlayerPrefs.SetInt("Depth", 0);
        }
        SetInstance();
        _healthOfPlayer = new HealthOfPlayer(health: 70, maxHaelthValue: 100);
        _scoreOfOlayer = new Score(0);

        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelLoad;
    }
    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnLevelLoad;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical);
        if (MapGenerator.instance)
            if (MapGenerator.instance.SceneIsLoad)
                return;

        if (!isDashing && !(PauseManager.instance && PauseManager.instance.onPause) && isInstantiate)
            _rigidBody.velocity = direction.normalized * speed * gameObject.GetComponent<Player>().speed;
        else if (!isDashing)
            _rigidBody.velocity = Vector3.zero;

        StartCoroutine(Dash(direction));
    }
    private void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {
        isInstantiate = false;
        if (SceneManager.GetActiveScene().name == "TestLobby")
        {
            isInstantiate = true;
            PlayerPrefs.SetInt("Depth", 0);
        }
        if (sceneName == "Training" ||
            sceneName == "Cave" ||
            sceneName == "Wasteland" ||
            sceneName == "Forest" ||
            sceneName == "Laboratory")
        {
            GetComponent<DangeCreater>().CreateDange(sceneName);
        }
        _rigidBody.transform.position = Vector3.zero;
        CameraController.instance.SetCurrentRoom();
        currentSceneName = sceneName;
        sceneName = null;
    }

    private IEnumerator Dash(Vector3 direction)
    {
        if (Input.GetAxisRaw("Roll") == 0) yield break;
        if (direction == Vector3.zero) yield break;
        if (isDashing) yield break;

        isDashing = true;

        float elapsedTime = 0;
        while (elapsedTime < dashTime)
        {
            _rigidBody.velocity = direction.normalized * dashSpeed;
            
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
        if(other.CompareTag("Portal"))
        {
            sceneName = currentSceneName;
        }
        if (other.CompareTag("Training") ||
           other.CompareTag("Cave") ||
           other.CompareTag("Forest") ||
           other.CompareTag("Wasteland") ||
           other.CompareTag("Laboratory"))
        {
            PlayerPrefs.SetInt("Depth", PlayerPrefs.GetInt("Depth") + 1);
            sceneName = other.tag;
            SceneManager.LoadScene("Dange");
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
                    for (int i = 0; i < currentRoom.transform.childCount; i++)
                        if (currentRoom.transform.GetChild(i).CompareTag("RightDoor"))
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

    public string SceneName
    {
        get { return sceneName; }
    }

    public string CurrentSceneName
    {
        get { return currentSceneName; }
    }

    public bool IsInstantiate
    {
        set { isInstantiate = value; }
    }

    public GameObject Weapon
    {
        get { return weaponManager; }
    }
}