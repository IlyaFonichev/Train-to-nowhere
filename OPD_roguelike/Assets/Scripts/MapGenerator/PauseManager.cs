using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isInstantiate;
    private bool pause;
    public static PauseManager instance;

    private void Awake()
    {
        isInstantiate = false;
        pause = false;
        SetInstance();
        gameObject.SetActive(false);
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
    private void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {
        isInstantiate = false;
        gameObject.SetActive(false);
    }
    public void Pause()
    {
        if (SceneManager.GetActiveScene().name != "TestLobby" && isInstantiate)
        {
            pause = !pause;
            gameObject.SetActive(pause);
        }
    }
    public void ExitInMenu()
    {
        gameObject.SetActive(false);
        pause = false;
        SceneManager.LoadScene("TestLobby");
    }

    public void Continue()
    {
        Pause();
    }

    public void openInventory()
    {
        gameObject.SetActive(false);
        InventoryMenu.instance.gameObject.SetActive(true);
    }

    public void setPause()
    {
        pause = !pause;
    }

    public bool IsInstantiate
    {
        set { isInstantiate = value; }
    }
    public bool onPause
    {
        get { return pause; }
    }
}
