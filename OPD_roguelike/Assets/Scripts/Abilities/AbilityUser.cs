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
    public static AbilityUser instance;
  
    private void Start()
    {
        instance = this;
    }
    public static void AddAbility(Ability newAbility)
    {
        instance.abilities.Add(newAbility);
        GameObject temp = Instantiate(instance.emptyAbilityPrefab);
        temp.transform.parent = instance.content.transform;
        temp.GetComponent<Image>().sprite = newAbility.getSprite;
        temp.GetComponent<Image>().color = newAbility.getColor;
        Debug.Log(newAbility.getType);
        Debug.Log(newAbility.getDescription);
    }
}
