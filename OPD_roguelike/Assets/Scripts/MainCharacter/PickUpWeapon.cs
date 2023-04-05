using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float accessRange;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite activeSprite;

    private SpriteRenderer sr;
    private GameObject curWeapon;

    private void Start()
    {
        player = PlayerController.instance.gameObject;

        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude < accessRange)
        {
            sr.sprite = activeSprite;
            if (Input.GetKeyDown(KeyCode.E))
            {
                curWeapon = player.GetComponent<InventoryScript>().firstWeapon;

                if (!curWeapon.GetComponent<WeaponScript>().getReloading()) 
                {
                    curWeapon.GetComponent<WeaponScript>().enabled = false;
                    curWeapon.GetComponent<PickUpWeapon>().enabled = true;

                    player.GetComponent<InventoryScript>().firstWeapon = gameObject;

                    gameObject.GetComponent<WeaponScript>().enabled = true;

                    StartCoroutine(waitPrint());

                    gameObject.GetComponent<PickUpWeapon>().enabled = false;
                }
            }
        }
        else
        {
            sr.sprite = defaultSprite;
        }
    }

    private IEnumerator waitPrint()
    {
        yield return new WaitForEndOfFrame();

        gameObject.GetComponent<WeaponScript>().printAmmo();

        yield break;
    }
}
