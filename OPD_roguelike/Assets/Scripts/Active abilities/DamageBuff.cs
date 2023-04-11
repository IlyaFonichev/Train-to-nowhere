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
        Player p = PlayerController.instance.gameObject.GetComponent<Player>();

        p.damageDealMultiplayer *= multiplayer;

        yield return new WaitForSeconds(time);

        p.damageDealMultiplayer /= multiplayer;
    }
}
