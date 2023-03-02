using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private static Text _HealthNum = GameObject.Find("HealthNum").GetComponent<Text>();
    private static Image _healthImage = GameObject.Find("DotherHealth").GetComponent<Image>();
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
            SceneManager.LoadScene("Death");
        }
        _HealthNum.text = _helthOfPlayer.ToString();
        _healthImage.transform.position = new Vector3(-0.7962963f-(2-_helthOfPlayer / 50f), _healthImage.transform.position.y, _healthImage.transform.position.z);
    }


    public static void AddHelth(uint addHelth)
    {
        _helthOfPlayer += addHelth;
        if (_helthOfPlayer > 100)
            _helthOfPlayer = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
