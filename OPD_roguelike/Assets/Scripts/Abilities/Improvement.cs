using UnityEngine;

public class Improvement : MonoBehaviour
{
    [SerializeField]
    private Ability ability;

    private void OnMouseDown()//��� ���, ��� ������������. �������� �� OnTriggerEnter
    {
        AbilityUser.AddAbility(ability);
        Destroy(gameObject);
    }
}
