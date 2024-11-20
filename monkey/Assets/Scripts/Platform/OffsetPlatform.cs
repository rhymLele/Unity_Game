using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPlatform : MonoBehaviour
{
    private Vector2 offset;
    [SerializeField] private float moveDistance = 3.0f;   // Distance to move the platform
    [SerializeField] private float speed = 2.5f;          // Speed of movement
    private bool moveRight = true;      // Direction control
    [SerializeField] private float minX = -6.5f;          // Minimum x boundary
    [SerializeField] private float maxX = 6.5f;
    private AudioController audioController;
    // Start is called before the first frame update
    void Start()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveRight = !moveRight;
            StartCoroutine(MovePlat());
        }
        if (collision.gameObject.CompareTag("Player") && !DestroyScript.isEquipped)
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                if (playerRb.velocity.y <= 0)
                {
                    playerRb.AddForce(Vector3.up * 400f);
                    if (audioController.jumpClip != null)
                    {
                        audioController.PlaySFX(audioController.jumpClip);
                    }
                }
            }
        }

    }

    private IEnumerator MovePlat()
    {
        Vector3 targetPosition;

        if (moveRight)
        {
            targetPosition = transform.position + new Vector3(moveDistance, 0, 0); // Move right
        }
        else
        {
            targetPosition = transform.position - new Vector3(moveDistance, 0, 0); // Move left
        }

        // Ensure the target position is within bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // Check if the platform reached the boundary and reverse direction if needed
        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            moveRight = !moveRight;
            StartCoroutine(MovePlat()); 
        }
    }

}
