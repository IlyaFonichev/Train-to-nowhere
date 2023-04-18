using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public float health = 100;                // health of player
    [HideInInspector] public float damage = 1;                  // damage multiplayer
    [HideInInspector] public float speed = 1;                   // player movement multiplayer
    [HideInInspector] public float fireSpeed = 1;               // fire speed multiplayer
    [HideInInspector] public float shotSpeed = 1;               // bullet speed multiplayer
    [HideInInspector] public float range = 1;                   // range multiplayer
    [HideInInspector] public float luck = 1;                    // affects drop chance
}
