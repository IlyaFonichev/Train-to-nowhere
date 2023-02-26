using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel, quitPanel;
    
    private void Start()
    {

        quitPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void Quit()
    {
        quitPanel.SetActive(true);
    }
    public void Reject()
    {
        quitPanel.SetActive(false);
    }
    public void Confirm()
    {
        Application.Quit();
    }
}
