using System.Collections.Generic;
using UnityEngine;

public class Decorations : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private List<Sprite> variant;
    public void SetSprite()
    {
        spriteRenderer.sprite = variant[Random.Range(0, variant.Count)];
        //spriteRenderer.transform.localScale = Vector3.one * 3;
        spriteRenderer.color = Color.white;
        Vector3 bcSize;
        spriteRenderer.gameObject.AddComponent<BoxCollider>();
        bcSize = spriteRenderer.gameObject.GetComponent<BoxCollider>().size;
        spriteRenderer.gameObject.GetComponent<BoxCollider>().size = new Vector3(bcSize.x / 1.3f, bcSize.y / 3, bcSize.z);
    }
}
