using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorSelected : MonoBehaviour
{
    private void OnMouseDown()//Это так, для демонстрации. Поменяем на OnTriggerEnter
    {
        SceneManager.LoadScene("TestLobby");
    }
}
