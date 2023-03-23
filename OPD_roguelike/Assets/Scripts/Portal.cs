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
    private void OnMouseDown()//Это так, для демонстрации. Поменяем на OnTriggerEnter
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Wasteland":
                PlayerPrefs.SetInt("WastelandDepth", PlayerPrefs.GetInt("WastelandDepth") + 1);
                if (PlayerPrefs.GetInt("WastelandDepth") % 5 == 0)
                    SceneManager.LoadScene("TestLobby");
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Forest":
                PlayerPrefs.SetInt("ForestDepth", PlayerPrefs.GetInt("ForestDepth") + 1);
                if (PlayerPrefs.GetInt("ForestDepth") % 3 == 0)
                    SceneManager.LoadScene("TestLobby");
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Laboratory":
                PlayerPrefs.SetInt("LaboratoryDepth", PlayerPrefs.GetInt("LaboratoryDepth") + 1);
                if(PlayerPrefs.GetInt("LaboratoryDepth") % 7 == 0)
                    SceneManager.LoadScene("TestLobby");
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            default:
                break;
        }
    }
}
