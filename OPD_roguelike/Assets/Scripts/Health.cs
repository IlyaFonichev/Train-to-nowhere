using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private static Text _HealthNum = GameObject.Find("HealthNum").GetComponent<Text>();
    private static Image _healthImage = GameObject.Find("DotherHealth").GetComponent<Image>();
    private static Image _Mather = GameObject.Find("MatherHealth").GetComponent<Image>();
    private static uint _helthOfPlayer = 100;

    public static uint GetHelth()
    {
        return _helthOfPlayer;
    }

    public static void HitToPlayer(uint damage)
    {
        _helthOfPlayer -= damage;
        if (_helthOfPlayer <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
        _HealthNum.text = _helthOfPlayer.ToString();
        // Получаем родительский объект
        Transform parentTransform = _Mather.transform.parent;

        // Сдвигаем дочерний объект относительно родителя
        _healthImage.transform.Translate(-0.0185f, 0f, 0f, parentTransform);
    }


    public static void AddHelth(uint addHelth)
    {
        _helthOfPlayer += addHelth;
        if (_helthOfPlayer > 100)
            _helthOfPlayer = 100;
    }

}
