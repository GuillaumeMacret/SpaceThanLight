using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { EMPTY,GUN,SHIELD,PILOT};

public class SpaceShipRoom : MonoBehaviour
{
    // Model
    public Spaceship ship;
    public RoomType type = RoomType.EMPTY;

    public int roomDamage = 0;
    public int roomHealth = 2;

    public SpaceShipRoom(RoomType type)
    {
        this.type = type;
    }

    public void Damage(int value)
    {
        roomDamage = Mathf.Clamp(roomDamage + value, 0, roomHealth);
        ship.Damage(value);        
    }


    // View
    public int m_RoomSize = 2;

    //AI
    public int m_RoomAiScore = 1;
}
