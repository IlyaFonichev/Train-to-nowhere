using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUser : MonoBehaviour
{
    private PassiveAbility ab;

    public void Visit(ConstSpeedBuff buff)
    {
        StartCoroutine(buff.ApplyBuff());
    }

    public void Leave(ConstSpeedBuff buff)
    {
        buff.DisableBuff();
    }
}
