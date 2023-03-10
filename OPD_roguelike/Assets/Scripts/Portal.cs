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
        PlayerPrefs.SetInt("Depth", PlayerPrefs.GetInt("Depth") + 1);
        SceneManager.LoadScene("Dange");
    }
}
