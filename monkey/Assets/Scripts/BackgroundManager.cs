using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    [SerializeField] private Sprite[] backgrounds, playerskin,bulletSkin; 
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
        Debug.Log("Map"+randomIndex);
        spriteRenderer.sprite = backgrounds[randomIndex];
        spritePlayer.sprite = playerskin[randomIndex];
    }
    private void Update()
    {
       
    }
}
