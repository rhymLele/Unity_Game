using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWarpScript : MonoBehaviour
{
    private bool isTeleporting = false; // Flag to prevent multiple teleports

    private void OnTriggerEnter2D(Collider2D obj)
    {
        // Check if the object that entered the trigger is the player
        if (obj.CompareTag("Player") && !isTeleporting)
        {
            isTeleporting = true; // Set the flag to true to prevent further teleports

            if (gameObject.CompareTag("rightWall"))
            {
                // Teleport to the left side
                obj.transform.position = new Vector2(-6f, obj.transform.position.y);
            }
            else if (gameObject.CompareTag("leftWall"))
            {
                // Teleport to the right side
                obj.transform.position = new Vector2(6f, obj.transform.position.y);
            }

            // Start cooldown to allow for a brief delay before allowing another teleport
            StartCoroutine(TeleportCooldown());
        }
    }

    private IEnumerator TeleportCooldown()
    {

        yield return new WaitForSeconds(0.5f);
        isTeleporting = false;
    }
}