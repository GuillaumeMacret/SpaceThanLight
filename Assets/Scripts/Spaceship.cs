using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public SpaceShipRoom[] m_Rooms;

    private void Awake()
    {
        m_Rooms = GetComponentsInChildren<SpaceShipRoom>();
    }

    public int health;
    public int damages;

    public void Damage(int value)
    {
        damages += value;
    }

}
