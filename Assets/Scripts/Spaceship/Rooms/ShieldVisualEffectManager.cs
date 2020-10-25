using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldVisualEffectManager : MonoBehaviour
{

    private static float SPRITE_PADDING = .5f;
    public GameObject[] sprites;
    public int activesprites = 0;
    public Image reloadProgressBar;

    public GameObject shieldSprite;

    private void Awake()
    {
        sprites = new GameObject[SpaceShipRoom.MAX_SHIELDS];
        for(int i = 0; i < SpaceShipRoom.MAX_SHIELDS; ++i)
        {
            sprites[i] = Instantiate(shieldSprite);
            sprites[i].gameObject.transform.SetParent(this.transform);
            sprites[i].gameObject.transform.position += new Vector3(i * SPRITE_PADDING + this.transform.position.x,this.transform.position.y);
            sprites[i].gameObject.SetActive(false);
        }
        UpdateSpriteStatus();
    }

    public void UpdateSpriteStatus()
    {
        for(int i = 0; i < SpaceShipRoom.MAX_SHIELDS; ++i)
        {
            sprites[i].SetActive(i < activesprites ? true : false);
        }
    }
}
