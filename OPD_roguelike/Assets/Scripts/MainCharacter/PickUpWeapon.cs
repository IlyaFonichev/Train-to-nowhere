using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float accessRange;

    private void Start()
    {
        player = PlayerController.instance.gameObject;

    }

    private void Update()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude < accessRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject storedCurWeapon = player.GetComponent<InventoryScript>().firstWeapon;
                if (storedCurWeapon != null)
                {
                    GameObject curWeapon = Instantiate(storedCurWeapon, transform.position,
                        Quaternion.Euler(90, 0, 0));
                    curWeapon.transform.SetParent(RoomSwitcher.instance.CurrentRoom.GetComponent<Room>().ObjectManager.transform);
                    curWeapon.GetComponent<WeaponScript>().enabled = false;
                    curWeapon.GetComponent<PickUpWeapon>().enabled = true;
                    curWeapon.name = player.GetComponent<InventoryScript>().firstWeapon.name;
                    //curWeapon.transform.localScale = new Vector3(0.3f, 0.3f, 1);

                    Destroy(player.GetComponent<InventoryScript>().firstWeapon);
                }

                GameObject weaponManager = PlayerController.instance.Weapon;
 
                GameObject newWeapon = Instantiate(gameObject, transform.position,
                    Quaternion.Euler(90, 0, 0));
                PlayerController.instance.Weapon.transform.rotation = Quaternion.Euler(0, 0, 0);
                newWeapon.transform.SetParent(weaponManager.transform);
                newWeapon.name = gameObject.name;
                newWeapon.GetComponent<PickUpWeapon>().enabled = false;
                newWeapon.GetComponent<WeaponScript>().enabled = true;
                //newWeapon.transform.localScale = Vector3.one;

                StartCoroutine(waitPrint(newWeapon));

                player.GetComponent<InventoryScript>().firstWeapon = newWeapon;

                Destroy(gameObject);
            }
        }
    }

    private IEnumerator waitPrint(GameObject obj)
    {
        yield return new WaitForEndOfFrame();

        obj.GetComponent<WeaponScript>().printAmmo();

        yield break;
    }
}
