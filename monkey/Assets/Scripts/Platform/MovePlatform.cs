using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Vector2 offset;
    [SerializeField] private float moveDistance = 3.0f;   // Distance to move the platform
    [SerializeField] private float speed = 2.5f;          // Speed of movement
    private bool moveRight = true;      // Direction control

    void OnMouseDown()
    {
        // Calculate the offset between the mouse position and the platform's position
        offset =(Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)-(Vector2)transform.position;
    }

    void OnMouseDrag()
    {
       transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)-offset;
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
                    //if (audioController.jumpClip != null)
                    //{
                    //    audioController.PlaySFX(audioController.jumpClip);
                    //}
                }
                /*else
                {
                    Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
                } */
            }
        }

    }

    private IEnumerator MovePlat()
    {
        Vector3 targetPosition;

        if (moveRight)
            targetPosition = transform.position + new Vector3(moveDistance, 0, 0); // Move right
        else
            targetPosition = transform.position - new Vector3(moveDistance, 0, 0); // Move left

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

}
