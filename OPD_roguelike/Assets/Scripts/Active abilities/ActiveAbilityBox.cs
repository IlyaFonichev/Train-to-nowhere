using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbilityBox : MonoBehaviour
{
    public static ActiveAbilityBox instance;

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
