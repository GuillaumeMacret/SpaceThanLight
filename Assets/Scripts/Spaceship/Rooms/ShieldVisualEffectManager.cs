using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldVisualEffectManager : MonoBehaviour
{
    private static readonly int MAX_SHIELDS = 5;

    private static int SPRITE_PADDING = 2;
    public GameObject[] sprites;
    public int activesprites = 0;

    public GameObject shieldSprite;

    private void Awake()
    {
        sprites = new GameObject[MAX_SHIELDS];
        for(int i = 0; i < MAX_SHIELDS; ++i)
        {
            sprites[i] = Instantiate(shieldSprite);
            sprites[i].gameObject.transform.SetParent(this.transform);
            sprites[i].gameObject.transform.position += new Vector3(i * SPRITE_PADDING,1);
            sprites[i].gameObject.SetActive(false);
        }
        UpdateSpriteStatus();
    }

    public void UpdateSpriteStatus()
    {
        Debug.Log(activesprites + " shield sprite   to show");
        for(int i = 0; i < MAX_SHIELDS; ++i)
        {
            sprites[i].SetActive(i < activesprites ? true : false);
        }
    }
}
