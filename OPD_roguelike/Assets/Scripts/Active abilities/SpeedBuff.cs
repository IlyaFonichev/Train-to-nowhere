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
        Player p = PlayerController.instance.gameObject.GetComponent<Player>();

        p.speedMultiplayer *= multiplayer;

        yield return new WaitForSeconds(time);

        p.speedMultiplayer /= multiplayer;
    }
}
