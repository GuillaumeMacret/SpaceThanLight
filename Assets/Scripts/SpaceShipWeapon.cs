using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceShipWeapon : MonoBehaviour
{
    private static readonly int DEFAULT_WEAPON_DAMAGE = 1;
    private static readonly float DEFAULT_WEAPON_COOLDOWN = 5;

    public int damages = DEFAULT_WEAPON_DAMAGE;
    public float cooldown = DEFAULT_WEAPON_COOLDOWN;
    public float cooldownTimer = DEFAULT_WEAPON_COOLDOWN;

    public SpaceShipRoom target;

    private void Update()
    {
        if(target != null && cooldownTimer <= 0)
        {
            Fire();
        }
        cooldownTimer = Mathf.Clamp(cooldownTimer - Time.deltaTime,0,cooldown);
    }

    private void Fire()
    {
        target.Damage(damages);
        cooldownTimer = cooldown;
    }

}
