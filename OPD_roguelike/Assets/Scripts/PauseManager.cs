using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool pause;
    public static PauseManager instance;

    private void Start()
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

    public void Pause()
    {
        pause = !pause;
        gameObject.SetActive(pause);
    }
    public void ExitInMenu()
    {
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
