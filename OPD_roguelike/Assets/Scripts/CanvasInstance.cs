using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasInstance : MonoBehaviour
{
    public static CanvasInstance instance;
    public GameObject activeAbilityBox, ammo;
    private void Awake()
    {
        SetInstance();
    }

    private void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name != "TestLobby")
            instance.gameObject.SetActive(false);
    }
    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnLevelLoad;
        }
        else
            Destroy(gameObject);
    }

    public void Show()
    {
        instance.gameObject.SetActive(true);
    }
}
