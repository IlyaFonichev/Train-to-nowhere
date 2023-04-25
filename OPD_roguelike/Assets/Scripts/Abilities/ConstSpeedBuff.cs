using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstSpeedBuff : PassiveAbility
{
    public override void Accept(AbilityUser visitor)
    {
        visitor.Visit(this);
    }

    public override void Discard(AbilityUser visitor)
    {
        visitor.Leave(this);
    }

    public override IEnumerator ApplyBuff()
    {
        Player p = PlayerController.instance.gameObject.GetComponent<Player>();

        PlayerController.instance.gameObject.GetComponent<InventoryScript>().buffsList.Add(this);

        p.speed *= multiplayer;

        yield break;
    }

    public override void DisableBuff()
    {
        Player p = PlayerController.instance.gameObject.GetComponent<Player>();

        PlayerController.instance.gameObject.GetComponent<InventoryScript>().buffsList.Remove(this);

        p.speed /= multiplayer;
    }
}
