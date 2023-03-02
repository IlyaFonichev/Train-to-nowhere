using UnityEngine;

public class NewLevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject gang;
    private void Start()
    {
        //         объект, Место спавна,  Поворот
        Instantiate(gang, Vector3.zero, Quaternion.Euler(90, 0, 0));
    }
}
