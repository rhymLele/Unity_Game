using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    private AudioController audioController;
    // Start is called before the first frame update
    private float originalGravityScale;
    public float slowGravityScale = 1f; // Gravity scale during jump to slow down ascent
    public float normalGravityScale = 3f; // Normal gravity scale after jump
    void Start()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !DestroyScript.isEquipped)
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                if (playerRb.velocity.y <= 0)
                {
                    playerRb.AddForce(Vector3.up * 400f);
                    originalGravityScale = playerRb.gravityScale;
                    playerRb.gravityScale = slowGravityScale;
                    StartCoroutine(ResetGravityScale(playerRb));
                    if (audioController.jumpClip != null)
                    {
                        audioController.PlaySFX(audioController.jumpClip);
                    }
                }
                /*else
                {
                    Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
                } */
            }
        }

    }
    private IEnumerator ResetGravityScale(Rigidbody2D playerRb)
    {
        // Wait for a short duration to let the player reach the peak of the jump
        yield return new WaitForSeconds(0.5f); // Adjust this duration for desired effect

        // Reset gravity scale back to normal
        playerRb.gravityScale = originalGravityScale;
    }
}
