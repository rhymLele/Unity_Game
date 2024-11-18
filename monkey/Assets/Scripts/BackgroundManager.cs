using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletBehaviour;

public class BackgroundManager : MonoBehaviour
{

    [SerializeField] private Sprite[] backgrounds, playerskin, playerShootSkin, bulletSkin; 
    private SpriteRenderer spriteRenderer,spritePlayer,spriteBullet;
    public static int randomIndex;
    private GameObject player;
    private BulletBehaviour bulletBehaviour;
    [SerializeField] private GameObject bullet;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            spritePlayer = player.GetComponent<SpriteRenderer>();
        }

        randomIndex = Random.Range(0, backgrounds.Length);
        Debug.Log("Map"+randomIndex);
        spriteRenderer.sprite = backgrounds[randomIndex];
        spritePlayer.sprite = playerskin[randomIndex];
        bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
        if (bulletBehaviour != null)
        {
            if (randomIndex == 1 || randomIndex == 2 || randomIndex == 3)
            {
                bulletBehaviour.ToggleBulletType(bulletType.Normal);
            }
            else
            {
                bulletBehaviour.ToggleBulletType(bulletType.Physics);
            }
        }
            
      
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
        if(ButtonSetting.selectedMode == "NightScene")
        {
            if (randomIndex >= 0 && randomIndex < playerShootSkin.Length)
            {
                return playerShootSkin[randomIndex];
            }
            return playerShootSkin[0];
        }
        else
        {
            return null;
        }
        
    }
    private void Update()
    {
       
    }
}
