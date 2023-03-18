using System.Collections.Generic;
using UnityEngine;

public class DecorationPart : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> decorationsPrefabs;
    public void Instantiation()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (Random.Range(0, 5) < 4)
            {
                Instantiate(decorationsPrefabs[Random.Range(0, decorationsPrefabs.Count)],
                    transform.GetChild(i).position,
                    Quaternion.identity).transform.SetParent(transform.parent);
            }
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
