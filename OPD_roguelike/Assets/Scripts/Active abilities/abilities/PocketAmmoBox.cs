using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketAmmoBox : ActiveAbility
{
    [SerializeField] private GameObject ammoBox;

    public override void Accept(ActiveAbilityVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override IEnumerator ApplyBuff()
    {
        if (curCoolDown > 0) yield break;

        curCoolDown = cooldown;

        Instantiate(ammoBox, PlayerController.instance.transform.position, Quaternion.Euler(90, 0, 0));
    }
}
