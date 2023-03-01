using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcInteraction : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject messagePrefab;
    [SerializeField] private float accessRange;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite activeSprite;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude < accessRange)
        {
            sr.sprite = activeSprite;
        }
        else
        {
            sr.sprite = defaultSprite;
        }
    }

    private void OnMouseExit()
    {
        sr.sprite = defaultSprite;
    }

    private void OnMouseUpAsButton()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude < accessRange)
        {
            GameObject message = Instantiate(messagePrefab, new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z + 1.5f), Quaternion.identity);
            
        }
    }
}
