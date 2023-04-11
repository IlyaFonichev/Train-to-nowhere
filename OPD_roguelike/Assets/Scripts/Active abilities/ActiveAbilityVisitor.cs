using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbilityVisitor : MonoBehaviour
{
    private ActiveAbility ab;

    private void Update()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.Q))
                if (gameObject.GetComponent<InventoryScript>().ability.TryGetComponent<ActiveAbility>(out ab))
                    ab.Accept(this);
        }
        catch { }
    }

    public void Visit(SpeedBuff buff)
    {
        StartCoroutine(buff.ApplyBuff());
    }

    public void Visit(DamageBuff buff)
    {
        StartCoroutine(buff.ApplyBuff());
    }

    public void Visit(VodkaBuff buff)
    {
        StartCoroutine(buff.ApplyBuff());
    }
}
