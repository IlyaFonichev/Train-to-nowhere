using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene(gameObject.name);
    }
}
