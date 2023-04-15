using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : ActiveAbility
{
    public override void Accept(ActiveAbilityVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override IEnumerator ApplyBuff()
    {
        if (curCoolDown > 0) yield break;

        curCoolDown = cooldown;

        Player p = PlayerController.instance.gameObject.GetComponent<Player>();

        p.damageDealMultiplayer *= multiplayer;

        yield return new WaitForSeconds(time);

        p.damageDealMultiplayer /= multiplayer;
    }
}
