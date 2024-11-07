using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    public Sprite[] backgrounds, playerskin,bulletSkin; 
    private SpriteRenderer spriteRenderer,spritePlayer,spriteBullet;
    public static int randomIndex;
     GameObject player;
    GameObject bullet;
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        bullet = GameObject.FindWithTag("bullet");
        if (player != null)
        {
            spritePlayer = player.GetComponent<SpriteRenderer>();
        }
        randomIndex = Random.Range(0, backgrounds.Length);
        Debug.Log(randomIndex);
        spriteRenderer.sprite = backgrounds[randomIndex];
        spritePlayer.sprite = playerskin[randomIndex];
        //spriteBullet.sprite =bulletSkin[randomIndex];  
        if (bullet != null)
        {
            //spriteBullet = bullet.GetComponent<SpriteRenderer>();
            // Set the bullet sprite based on the random index
            //spriteBullet.sprite = bulletSkin[randomIndex];
            // Get the BulletBehaviour component
            BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
            if (bulletBehaviour != null)
            {
               // Set the bullet type based on the random index
                if (randomIndex == 0 || randomIndex == 1 )
                {
                    bulletBehaviour.SetBulletType(BulletBehaviour.bulletType.Normal);
                }
                else
                {
                    bulletBehaviour.SetBulletType(BulletBehaviour.bulletType.Physics);
                }
            }
        }
    }
    private void Update()
    {
       
    }
}
