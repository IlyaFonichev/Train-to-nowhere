using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpSpeedBuff : ActiveAbility
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

        PlayerController.instance.gameObject.GetComponent<InventoryScript>().buffsList.Add(this);

        yield return new WaitForSeconds(time);

        PlayerController.instance.gameObject.GetComponent<InventoryScript>().buffsList.Remove(this);

        p.speed /= multiplayer;
    }
}
