using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    public Sprite[] backgrounds; 
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        int randomIndex = Random.Range(0, backgrounds.Length);

        spriteRenderer.sprite = backgrounds[randomIndex];
    }
}
