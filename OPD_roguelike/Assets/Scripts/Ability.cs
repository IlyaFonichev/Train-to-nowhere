using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected float multiplayer;

    public virtual void Accept(ActiveAbilityVisitor visitor) { }

    public virtual void Accept(AbilityUser visitor) { }

    public abstract IEnumerator ApplyBuff();

    public virtual void Discard(AbilityUser visitor) { }

    public virtual void DisableBuff() { }
}
