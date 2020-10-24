using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { EMPTY,GUN,SHIELD,PILOT};

public class SpaceShipRoom : MonoBehaviour
{
    private static readonly float SHIELD_RELOAD_TIME = 2.5f;

    // Model
    public Spaceship ship;
    public RoomType type = RoomType.EMPTY;

    public int roomDamage = 0;
    public int roomHealth = 2;

    /* Shield max value */
    public int roomShieldDamage = 0;
    /* Damage inflicted to shields */
    public int roomShield = 0;
    private float m_shieldReloadCpt = 0;
    public ShieldVisualEffectManager shieldVisualEffect;

    public SpaceShipRoom(RoomType type)
    {
        this.type = type;
    }

    public void Damage(int value)
    {
        //Debug.Log(name + "took " + value + " dammages");
        if(roomShield > roomShieldDamage)
        {
            int oldShieldVal = roomShieldDamage;
            roomShieldDamage = Mathf.Clamp(roomShieldDamage + value, 0, roomShield);
            shieldVisualEffect.activesprites -= (roomShieldDamage - oldShieldVal);
            shieldVisualEffect.UpdateSpriteStatus();
            value -= roomShieldDamage - oldShieldVal;
        }
        roomDamage = Mathf.Clamp(roomDamage + value, 0, roomHealth);
        ship.Damage(value);        
    }

    public void AddShieldPower()
    {
        roomShield++;
    }

    /**
     * <summary>Tries to remove 1 max shield power from the room</summary>
     * <returns>1 if you can remove power, 0 if you can't</returns>
     **/
    public int RemoveShieldPower()
    {
        if (roomShield > roomShieldDamage)
        {
            roomShield--;
            return 1;
        }
        return 0;
    }

    private void Update()
    {
        ReloadShield();
    }

    /**<summary>
     * Adds delta time to shield reload counter
     * Decreases shield damages if needed
     * </summary>
     **/
    private void ReloadShield()
    {
        if (roomShieldDamage != 0)
        {
            m_shieldReloadCpt += Time.deltaTime;
        }
        if (m_shieldReloadCpt >= SHIELD_RELOAD_TIME)
        {
            roomShieldDamage -= 1;
            m_shieldReloadCpt = 0;
            shieldVisualEffect.activesprites = roomShield - roomShieldDamage;
            shieldVisualEffect.UpdateSpriteStatus();
        }
    }

    // Controller

    public void OnClick(Spaceship playerShip)
    {
        //Debug.Log("Set target on " + this.name);
        playerShip.SetTarget(this);
    }



    // View
    public int m_RoomSize = 2;

    //AI
    public int m_RoomAiScore = 1;
}
