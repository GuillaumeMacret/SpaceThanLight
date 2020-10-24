using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public Spaceship playership;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            //Debug.Log("Click " + mousePos2D);

            if (hit.collider != null)
            {
                //Debug.Log("Clicked on " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.GetComponent<SpaceShipRoom>() != null)
                {
                    //Debug.Log("Wich is clickable");
                    SpaceShipRoom click = hit.collider.gameObject.GetComponent<SpaceShipRoom>();
                    click.OnClick(playership);
                }
            }
        }
    }
}
