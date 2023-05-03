using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class OriginRoom : MonoBehaviour
{
    [SerializeField]
    private List<SpriteRenderer> doorSprites;
    public void Recolor(Color color)
    {
        for (int i = 0; i < doorSprites.Count; i++)
            doorSprites[i].color = color;
    }
}
