using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbility : MonoBehaviour
{
    [SerializeField] private Ability.ActiveAbilityType Type;
    [SerializeField] private float time;
    [SerializeField] private float multiplayer;
    [SerializeField] private float cooldown;

    private Player player;
    private bool abilityIsActive = false;
    private float timer = 0;

    private void Start()
    {
        player = PlayerController.instance.gameObject.GetComponent<Player>();
        timer = cooldown;
    }

    private void Update()
    {
        if (timer > cooldown)
            StartCoroutine(applyMultiplayer());
        else
            timer += Time.deltaTime;
    }

    private IEnumerator applyMultiplayer()
    {
        if (abilityIsActive) yield break;
        if (!Input.GetKeyDown(KeyCode.Q)) yield break;

        abilityIsActive = true;
        timer = 0;

        switch (Type)
        {
            case Ability.ActiveAbilityType.godMode:
                player.speedMultiplayer *= multiplayer;
                break;
            case Ability.ActiveAbilityType.ammoBox:
                player.speedMultiplayer *= multiplayer;
                break;
            case Ability.ActiveAbilityType.damageUp:
                player.damageDealMultiplayer *= multiplayer;
                break;
            case Ability.ActiveAbilityType.infinityAmmo:
                player.speedMultiplayer *= multiplayer;
                break;
            case Ability.ActiveAbilityType.speedUp:
                player.speedMultiplayer *= multiplayer;
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(time);

        switch (Type)
        {
            case Ability.ActiveAbilityType.godMode:
                player.speedMultiplayer /= multiplayer;
                break;
            case Ability.ActiveAbilityType.ammoBox:
                player.speedMultiplayer /= multiplayer;
                break;
            case Ability.ActiveAbilityType.damageUp:
                player.damageDealMultiplayer /= multiplayer;
                break;
            case Ability.ActiveAbilityType.infinityAmmo:
                player.speedMultiplayer /= multiplayer;
                break;
            case Ability.ActiveAbilityType.speedUp:
                player.speedMultiplayer /= multiplayer;
                break;
            default:
                break;
        }

        abilityIsActive = false;

        yield break;
    }
}