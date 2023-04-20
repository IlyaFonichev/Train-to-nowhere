using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private float angle;
    private void Start()
    {
        angle = transform.rotation.y;
    }
    private void Update()//Изи анимация
    {
        angle += (Time.deltaTime * 60f) % 360;
        transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            switch (PlayerController.instance.CurrentSceneName)
            {
                case "Wasteland":
                    PlayerPrefs.SetInt("Depth", PlayerPrefs.GetInt("Depth") + 1);
                    if (PlayerPrefs.GetInt("Depth") % 5 == 0)
                    {
                        SceneManager.LoadScene("TestLobby"); 
                        PlayerPrefs.SetInt("Depth", 0);
                    }
                    else
                        SceneManager.LoadScene("Dange");
                    break;
                case "Forest":
                    PlayerPrefs.SetInt("Depth", PlayerPrefs.GetInt("Depth") + 1);
                    if (PlayerPrefs.GetInt("Depth") % 3 == 0)
                    { 
                        SceneManager.LoadScene("TestLobby");
                        PlayerPrefs.SetInt("Depth", 0);
                    }
                    else
                        SceneManager.LoadScene("Dange");
                    break;
                case "Laboratory":
                    PlayerPrefs.SetInt("Depth", PlayerPrefs.GetInt("Depth") + 1);
                    if (PlayerPrefs.GetInt("Depth") % 7 == 0)
                    { 
                        SceneManager.LoadScene("TestLobby");
                        PlayerPrefs.SetInt("Depth", 0);
                    }
                    else
                        SceneManager.LoadScene("Dange");
                    break;
                default:
                    break;
            }
        }
    }
}
