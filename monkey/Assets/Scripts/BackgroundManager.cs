using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    [SerializeField] private Sprite[] backgrounds, playerskin, playerShootSkin, bulletSkin; 
    private SpriteRenderer spriteRenderer,spritePlayer,spriteBullet;
    public static int randomIndex;
    private GameObject player;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            spritePlayer = player.GetComponent<SpriteRenderer>();
        }

        randomIndex = Random.Range(0, backgrounds.Length);
        Debug.Log(randomIndex);

        spriteRenderer.sprite = backgrounds[randomIndex];
        spritePlayer.sprite = playerskin[randomIndex];
    }

    public Sprite GetPlayerNormalSprite()
    {
        if (randomIndex >= 0 && randomIndex < playerskin.Length)
        {
            return playerskin[randomIndex];
        }
        return playerskin[0];
    }

    public Sprite GetPlayerShootSprite()
    {
        if (randomIndex >= 0 && randomIndex < playerShootSkin.Length)
        {
            return playerShootSkin[randomIndex];
        }
        return playerShootSkin[0];
    }
    private void Update()
    {
       
    }
}
