using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUser : MonoBehaviour
{
    [SerializeField]
    private GameObject emptyAbilityPrefab;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private List<Ability> abilities;
    private void Start()
    {
        foreach(Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < abilities.Count; i++)
        {
            GameObject newAbility = Instantiate(emptyAbilityPrefab);
            newAbility.transform.parent = content.transform;
            newAbility.GetComponent<Image>().sprite = abilities[i].Sprite;
            Debug.Log(abilities[i].Type);
            Debug.Log(abilities[i].Description);
        }
    }
}
