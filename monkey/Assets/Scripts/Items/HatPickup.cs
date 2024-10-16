using System.Collections;
using UnityEngine;

public class HatPickup : MonoBehaviour
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
        // Reset vertical velocity to ensure upward force is effective
        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0); // Reset vertical velocity if falling
        }

        // Apply upward force
        playerRb.AddForce(Vector2.up * 800f);
    }
}