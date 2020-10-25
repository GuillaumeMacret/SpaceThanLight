using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public SpaceShipRoom[] m_Rooms;
    public SpaceShipWeapon[] m_Weapons;

    private void Awake()
    {
        m_Rooms = GetComponentsInChildren<SpaceShipRoom>();
        m_Weapons = GetComponentsInChildren<SpaceShipWeapon>();
    }
    
    public int health;
    public int damages;

    public void Damage(int value)
    {
        damages += value;
    }

    public void SetTarget(SpaceShipRoom room, int weaponId)
    {
        //Debug.Log("Setting target for " + m_Weapons.Length + " weapons");
        if(weaponId < m_Weapons.Length)
        {
            m_Weapons[weaponId].SetTarget(room);
        }
    }

}
