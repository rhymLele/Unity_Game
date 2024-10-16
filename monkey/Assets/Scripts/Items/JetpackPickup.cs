using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                FlyUp(playerRb);
            }
            Destroy(gameObject);
        }
    }

    private void FlyUp(Rigidbody2D playerRb)
    {
        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0); 
        }

        // Apply upward force
        playerRb.AddForce(Vector2.up * 1500f);
    }
}
