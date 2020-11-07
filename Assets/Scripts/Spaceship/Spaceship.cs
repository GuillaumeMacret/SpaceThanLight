using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spaceship : MonoBehaviour
{
    [Header("Model")]
    public SpaceShipRoom[] rooms;
    public SpaceShipWeapon[] weapons;

    public int health;
    public int damages;

    public int maxPower;
    public int maxShieldPower = 2;
    public int maxWeaponPower = 0;

    private bool autofire = false;

    [Header("UI")]
    public Text shipHpText;

    private void Awake()
    {
        rooms = GetComponentsInChildren<SpaceShipRoom>();
        weapons = GetComponentsInChildren<SpaceShipWeapon>();
        UpdateHpText();
    }

    public void Damage(int value)
    {
        damages += value;
        UpdateHpText();
    }

    public void SetTarget(SpaceShipRoom room, int weaponId)
    {
        //Debug.Log("Setting target for " + m_Weapons.Length + " weapons");
        if(weaponId < weapons.Length)
        {
            weapons[weaponId].SetTarget(room);
        }
    }

    public int getAllocatedPower()
    {
        int sum = 0;
        foreach (SpaceShipRoom room in rooms)
        {
            sum += room.roomShield;
        }
        return sum;
    }

    /**
     * Computes if we have enough energy to allocate power
     **/
    public bool CanAllocateShieldPower()
    {
        int allocated = getAllocatedPower();
        if (allocated >= maxShieldPower) return false;
        return true;
    }

    private void UpdateHpText()
    {
        shipHpText.text = string.Format("Health : {0} / {1}", health - damages, health);

    }

    public void SwitchAutoFire()
    {
        autofire = !autofire;
        foreach(SpaceShipWeapon weap in weapons)
        {
            weap.SetAutofire(autofire);
        }
    }
}
