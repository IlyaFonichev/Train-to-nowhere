using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityAmmo : ActiveAbility
{
    public override void Accept(ActiveAbilityVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override IEnumerator ApplyBuff()
    {
        if (curCoolDown > 0) yield break;

        curCoolDown = cooldown;

        WeaponScript curWeapon = PlayerController.instance.gameObject.GetComponent<InventoryScript>().firstWeapon.GetComponent<WeaponScript>();
        int magazine = curWeapon.currMagazine;

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            curWeapon.currMagazine = magazine;

            elapsedTime += Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield break;
    }
}
