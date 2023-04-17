using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VodkaBuff : ActiveAbility
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

        p.damage *= multiplayer;
        p.speed /= multiplayer;

        yield return new WaitForSeconds(time);

        p.damage /= multiplayer;
        p.speed *= multiplayer;
    }
}
