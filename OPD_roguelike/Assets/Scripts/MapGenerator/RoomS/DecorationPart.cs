using System.Collections.Generic;
using UnityEngine;

public class DecorationPart : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> trees;
    [SerializeField]
    private GameObject treesPositions;
    [SerializeField]
    private GameObject dekorPositions;
    [SerializeField]
    private List<GameObject> decorationsPrefabs;
    public void Instantiation()
    {
        for (int i = 0; i < dekorPositions.transform.childCount; i++)
        {
            if (Random.Range(0, 5) < 4)
            {
                GameObject dekor = Instantiate(decorationsPrefabs[Random.Range(0, decorationsPrefabs.Count)],
                    dekorPositions.transform.GetChild(i).position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)),
                    Quaternion.identity);
                dekor.transform.SetParent(transform.parent);
                dekor.transform.localScale = new Vector3(dekor.transform.localScale.x * Mathf.Pow(-1, Random.Range(0, 2)), dekor.transform.localScale.y, dekor.transform.localScale.z);
            }
            Destroy(dekorPositions.transform.GetChild(i).gameObject);
        }
        transform.localScale = new Vector3(Mathf.Pow(-1, Random.Range(0, 2)) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        TreesInstantiate();
    }

    private void TreesInstantiate()
    {
        for(int i = 0; i < treesPositions.transform.childCount; i++)
        {
            treesPositions.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = trees[Random.Range(0, trees.Count)];
        }    
    }
}
