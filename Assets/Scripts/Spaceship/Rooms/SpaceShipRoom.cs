using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { EMPTY,GUN,SHIELD,PILOT};

public class SpaceShipRoom : MonoBehaviour
{
    private static readonly float DEFAULT_SHIELD_RELOAD_TIME = 2.5f;
    public static readonly int MAX_SHIELDS = 5;

    // Model
    public Spaceship ship;
    public RoomType type = RoomType.EMPTY;

    public int roomDamage = 0;
    public int roomHealth = 2;

    /* Shield max value */
    public int roomShieldDamage = 0;
    /* Damage inflicted to shields */
    public int roomShield = 0;
    public float shieldReloadTime = DEFAULT_SHIELD_RELOAD_TIME;
    private float m_shieldReloadCpt = 0;
    public ShieldVisualEffectManager visualEffectManager;

    private void Awake()
    {
        visualEffectManager.activesprites = roomShield - roomShieldDamage;
        visualEffectManager.UpdateSpriteStatus();
        ship = GetComponentInParent<Spaceship>();
    }

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
            visualEffectManager.activesprites -= (roomShieldDamage - oldShieldVal);
            visualEffectManager.UpdateSpriteStatus();
            value -= roomShieldDamage - oldShieldVal;
        }
        roomDamage = Mathf.Clamp(roomDamage + value, 0, roomHealth);
        ship.Damage(value);        
    }

    public void AddShieldPower()
    {
        if(roomShield < MAX_SHIELDS && ship.CanAllocateShieldPower())
        {
            roomShield++;
        }
        visualEffectManager.activesprites = roomShield - roomShieldDamage;
        visualEffectManager.UpdateSpriteStatus();
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
            visualEffectManager.activesprites = roomShield - roomShieldDamage;
            visualEffectManager.UpdateSpriteStatus();
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
            m_shieldReloadCpt = Mathf.Clamp(m_shieldReloadCpt + Time.deltaTime, 0, shieldReloadTime);
        }
        if (m_shieldReloadCpt >= shieldReloadTime && roomShieldDamage > 0)
        {
            roomShieldDamage -= 1;
            m_shieldReloadCpt = 0;
            //FIXME NullReferenceException: Object reference not set to an instance of an object
            visualEffectManager.activesprites = roomShield - roomShieldDamage;
            visualEffectManager.UpdateSpriteStatus();
        }
        
        visualEffectManager.reloadProgressBar.fillAmount = m_shieldReloadCpt / shieldReloadTime;
    }

    // Controller

    public void SetAsTargetFor(Spaceship playerShip, int weaponId)
    {
        //Debug.Log("Set target on " + this.name);
        playerShip.SetTarget(this, weaponId);
    }

    public void Untarget(Spaceship playerShip, int weaponId)
    {
        playerShip.SetTarget(null, weaponId);
    }


    // View
    public int m_RoomSize = 2;

    //AI
    public int m_RoomAiScore = 1;
}
