using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    [HideInInspector] public GameObject firstWeapon;
    [HideInInspector] public GameObject ability;
    [HideInInspector] public List<Ability> buffsList = new List<Ability>();
}