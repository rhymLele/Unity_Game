using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float BulletSpeed = 10f;
    [SerializeField] private float DestroyTime = 2.5f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDestroyTime();
        SetStraightVelocity();
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject,DestroyTime);
    }

    private void SetStraightVelocity()
    {
        rb.velocity=transform.up*BulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an enemy
        if (other.CompareTag("enemy"))
        {
            // Destroy the enemy GameObject
            Destroy(other.gameObject,0.5f);

            // Destroy the bullet itself
            Destroy(gameObject);
        }
    }
}
