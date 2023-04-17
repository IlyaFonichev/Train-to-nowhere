using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : ActiveAbility
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

        p.speed *= multiplayer;

        yield return new WaitForSeconds(time);

        p.speed /= multiplayer;
    }
}
