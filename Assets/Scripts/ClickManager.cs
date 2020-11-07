using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public Spaceship playership;

    public int LastDigitInput = -1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            //Debug.Log("Click " + mousePos2D);

            if (hit.collider != null)
            {
                //Debug.Log("Clicked on " + hit.collider.gameObject.name);
                SpaceShipRoom room = hit.collider.gameObject.GetComponent<SpaceShipRoom>();
                if (room != null)
                {
                    if (!room.GetComponentInParent<Spaceship>().CompareTag("Player") && LastDigitInput >= 0)
                    {

                        if (Input.GetMouseButtonDown(0))
                        {
                            room.SetAsTargetFor(playership, LastDigitInput);
                        }
                        else if (Input.GetMouseButtonDown(1))
                        {
                            room.Untarget(playership, LastDigitInput);
                        }
                        LastDigitInput = -1;
                    }
                    else if (room.GetComponentInParent<Spaceship>().CompareTag("Player"))
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            room.AddShieldPower();
                        }
                        else if (Input.GetMouseButtonDown(1))
                        {
                            room.RemoveShieldPower();
                        }
                    }

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { LastDigitInput = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { LastDigitInput = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { LastDigitInput = 2; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { LastDigitInput = 3; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { LastDigitInput = 4; }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { LastDigitInput = 5; }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { LastDigitInput = 6; }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { LastDigitInput = 7; }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { LastDigitInput = 8; }
        if (Input.GetKeyDown(KeyCode.Alpha0)) { LastDigitInput = 9; }
        if (Input.GetKeyDown(KeyCode.A)) { playership.SwitchAutoFire(); }
    }
}
