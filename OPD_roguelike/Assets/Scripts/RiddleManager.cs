using System.Collections.Generic;
using UnityEngine;

public class RiddleManager : MonoBehaviour
{
    [SerializeField]
    private List<char> checkKey;
    [SerializeField]
    private List<char> inputKey;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.DownArrow)
            || Input.GetKeyDown(KeyCode.RightArrow)
            || Input.GetKeyDown(KeyCode.LeftArrow))
            InputKey();
    }
    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            inputKey.Add('u');
        if (Input.GetKeyDown(KeyCode.DownArrow))
            inputKey.Add('d');
        if (Input.GetKeyDown(KeyCode.RightArrow))
            inputKey.Add('r');
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            inputKey.Add('l');

        if(inputKey.Count > checkKey.Count)
            inputKey.RemoveAt(0);

        if (CompareKeys())
        {
            Debug.Log("Правильный ключ");
        }
    }

    private bool CompareKeys()
    {
        if (inputKey.Count != checkKey.Count)
            return false;
        for (int i = 0; i < inputKey.Count; i++)
            if (inputKey[i] != checkKey[i])
                return false;
        return true;
    }
}
