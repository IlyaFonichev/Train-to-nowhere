using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveAbilityVisitor : MonoBehaviour
{
    private ActiveAbility ab;

    private void OnLevelWasLoaded()
    {
        if (gameObject.GetComponent<InventoryScript>().ability != null)
            GameObject.Find("ActiveAbilityBox").GetComponent<Image>().sprite = gameObject.GetComponent<InventoryScript>().ability.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.Q))
                if (gameObject.GetComponent<InventoryScript>().ability.TryGetComponent<ActiveAbility>(out ab) && (PauseManager.instance == null || !PauseManager.instance.GetComponent<PauseManager>().onPause))
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

    public void Visit(PocketAmmoBox buff)
    {
        StartCoroutine(buff.ApplyBuff());
    }

    public void Visit(InfinityAmmo buff)
    {
        StartCoroutine(buff.ApplyBuff());
    }
}
