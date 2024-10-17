using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackPickUp : MonoBehaviour
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
            if (audioController != null && audioController.baloClip != null)
            {
                audioController.PlaySFX(audioController.baloClip);
            }
            else
            {
                Debug.LogError("baloClip chưa được gán trong AudioController!");
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
        playerRb.AddForce(Vector2.up * 1600f);
    }
}
