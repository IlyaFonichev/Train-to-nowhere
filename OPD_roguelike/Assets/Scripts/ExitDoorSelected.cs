using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorSelected : MonoBehaviour
{
    private void OnMouseDown()//��� ���, ��� ������������. �������� �� OnTriggerEnter
    {
        SceneManager.LoadScene("TestLobby");
    }
}
