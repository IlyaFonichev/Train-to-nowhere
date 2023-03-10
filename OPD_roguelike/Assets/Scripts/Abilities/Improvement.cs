using UnityEngine;

public class Improvement : MonoBehaviour
{
    [SerializeField]
    private Ability ability;

    private void OnMouseDown()//Это так, для демонстрации. Поменяем на OnTriggerEnter
    {
        AbilityUI.AddAbility(ability);
        Destroy(gameObject);
    }
}
