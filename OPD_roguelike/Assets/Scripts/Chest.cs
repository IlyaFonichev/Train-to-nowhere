using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private List<GameObject> drops = new List<GameObject>();
    private int pickedWeapon;
    private GameObject tmp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickedWeapon = Random.Range(0, drops.Count);

            tmp = Instantiate(drops[pickedWeapon], transform.position, Quaternion.Euler(90, 0, 0));
            tmp.name = drops[pickedWeapon].name;
            tmp.transform.SetParent(RoomSwitcher.instance.CurrentRoom.GetComponent<Room>().ObjectManager.transform);

            Destroy(gameObject);
        }
    }
}
