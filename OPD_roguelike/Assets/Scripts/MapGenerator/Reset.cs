using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("ForestDepth", 0);
        PlayerPrefs.SetInt("WastelandDepth", 0);
        PlayerPrefs.SetInt("LaboratoryDepth", 0);
        SceneManager.LoadScene("TestLobby");
    }
}
