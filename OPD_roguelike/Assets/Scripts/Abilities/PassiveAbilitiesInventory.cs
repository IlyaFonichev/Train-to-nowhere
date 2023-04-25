using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassiveAbilitiesInventory : MonoBehaviour
{
    public static PassiveAbilitiesInventory instance;

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
