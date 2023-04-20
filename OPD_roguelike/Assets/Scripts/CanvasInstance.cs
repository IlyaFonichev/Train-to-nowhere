using UnityEditor.SearchService;
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

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name != "TestLobby")
            instance.gameObject.SetActive(false);
    }
    private void SetInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void Show()
    {
        instance.gameObject.SetActive(true);
    }
}
