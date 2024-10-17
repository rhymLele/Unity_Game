using System.Collections;
using UnityEngine;

public class HatPickup : MonoBehaviour
{
    private AudioController audioController;
    private void Start()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                FlyUp(playerRb);
            }

            if (audioController != null && audioController.mubayClip != null)
            {
                audioController.PlaySFX(audioController.mubayClip);
            }
            else
            {
                Debug.LogError("mubayClip chưa được gán trong AudioController!");
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
        playerRb.AddForce(Vector2.up * 1200f);
    }
}