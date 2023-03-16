using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float accessRange;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite activeSprite;

    private SpriteRenderer sr;

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
                gameObject.AddComponent<WeaponScript>();
                gameObject.GetComponent<PickUpWeapon>().enabled = false;
            }
        }
        else

        {
            sr.sprite = defaultSprite;
        }
    }
}
