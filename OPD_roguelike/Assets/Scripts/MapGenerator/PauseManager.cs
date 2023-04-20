using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool pause;
    public static PauseManager instance;

    private void Awake()
    {
        pause = false;
        SetInstance();
        gameObject.SetActive(false);
    }
    private void SetInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void OnLevelWasLoaded(int level)
    {
        gameObject.SetActive(false);
    }
    public void Pause()
    {
        if (SceneManager.GetActiveScene().name != "TestLobby")
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

    public bool onPause
    {
        get { return pause; }
    }
}
