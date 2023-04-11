using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : MonoBehaviour
{
    [SerializeField] protected float multiplayer;
    [SerializeField] protected float time;
    [SerializeField] protected float cooldown;

    public abstract void Accept(ActiveAbilityVisitor visitor);
    public abstract IEnumerator ApplyBuff();
}
