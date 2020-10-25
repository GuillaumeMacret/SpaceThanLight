using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceShipWeapon : MonoBehaviour
{
    private static readonly int DEFAULT_WEAPON_DAMAGE = 1;
    private static readonly float DEFAULT_WEAPON_COOLDOWN = 5;

    public int damages = DEFAULT_WEAPON_DAMAGE;
    public float cooldown = DEFAULT_WEAPON_COOLDOWN;
    private float cooldownTimer = 0;

    private SpaceShipRoom target;
    public GameObject targetCrosshair;
    public WeaponVisualEffectManager visualEffectManager;

    private void Awake()
    {
        targetCrosshair.SetActive(false);
    }

    private void Update()
    {
        if(target != null && cooldownTimer >= cooldown)
        {
            Fire();
        }
        if(cooldownTimer < cooldown)
        {
            cooldownTimer = Mathf.Clamp(cooldownTimer + Time.deltaTime, 0, cooldown);
        }
        visualEffectManager.reloadProgressBar.fillAmount = cooldownTimer / cooldown;
    }

    private void Fire()
    {
        target.Damage(damages);
        cooldownTimer = 0;
        target = null;
        targetCrosshair.SetActive(false);
    }

    public void SetTarget(SpaceShipRoom target)
    {
        this.target = target;
        if (target != null)
        {
            targetCrosshair.gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, targetCrosshair.gameObject.transform.position.z);
            targetCrosshair.SetActive(true);
        }
        else
        {
            targetCrosshair.SetActive(false);
        }
        
    }
}
