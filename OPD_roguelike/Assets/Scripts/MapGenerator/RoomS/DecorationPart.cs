using System.Collections.Generic;
using UnityEngine;

public class DecorationPart : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> decorationsPrefabs;
    [SerializeField]
    private GameObject spawnPointsDecorations;
    public void Instantiation()
    {
        for (int i = 0; i < spawnPointsDecorations.transform.childCount; i++)
        {
            if (Random.Range(0, 5) < 4)
            {
                Instantiate(decorationsPrefabs[Random.Range(0, decorationsPrefabs.Count)],
                    spawnPointsDecorations.transform.GetChild(i).position,
                    Quaternion.identity).transform.SetParent(transform);
            }
        }
        Destroy(spawnPointsDecorations);
    }
}
