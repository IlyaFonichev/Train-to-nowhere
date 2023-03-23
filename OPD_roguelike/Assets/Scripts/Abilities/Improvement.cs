using UnityEngine;

public class Improvement : MonoBehaviour
{
    [SerializeField]
    private Ability ability;

    private void OnMouseDown()//Это так, для демонстрации. Поменяем на OnTriggerEnter
    {
        AbilityUser.AddAbility(ability);
        Destroy(gameObject);
    }
}
