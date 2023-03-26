using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float accessRange;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite activeSprite;

    private SpriteRenderer sr;
    private Transform curWeapon;
    private TextAsset storedProperties;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude < accessRange)
        {
            sr.sprite = activeSprite;
            if (Input.GetKeyDown(KeyCode.E))
            {
                curWeapon = player.transform.GetChild(2);

                sr.sprite = curWeapon.GetComponent<SpriteRenderer>().sprite;
                curWeapon.GetComponent<SpriteRenderer>().sprite = defaultSprite;
                defaultSprite = sr.sprite;
                activeSprite = sr.sprite;

                storedProperties = gameObject.GetComponent<Item>().getItemProperties();

                gameObject.GetComponent<Item>().setItemProperties(curWeapon.GetComponent<Item>().getItemProperties());
                curWeapon.GetComponent<Item>().setItemProperties(storedProperties); 
                
                curWeapon.GetComponent<WeaponScript>().applyParameters();
            }
        }
        else
        {
            sr.sprite = defaultSprite;
        }
    }
}
