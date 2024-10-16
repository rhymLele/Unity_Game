using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has collided with the enemy
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    // Get the relative velocity to determine if the player is falling onto the enemy
        //    if (collision.relativeVelocity.y <= 0) // Player is falling down
        //    {
        //        // Player kills the enemy by jumping on top
        //        Destroy(gameObject);
        //    }
        //    else
        //    {
        //        // Player dies if hit from the side or below
        //        PlayerDie(collision.gameObject);
        //    }
        //}
    }

    private void PlayerDie(GameObject player)
    {
        // Handle player death (e.g., restart the game, lose a life, etc.)
        // For now, we just destroy the player to simulate death
        Destroy(player);
        Debug.Log("Player died.");
    }
}
