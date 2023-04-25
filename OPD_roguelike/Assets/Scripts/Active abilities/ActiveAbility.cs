using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : Ability
{
    [SerializeField] protected float time;
    [SerializeField] protected int cooldown;
    [HideInInspector] public int curCoolDown = 0;
}
