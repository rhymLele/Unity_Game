using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Vector2 offset;
    private float dragTimer = 0f;       // Timer to track drag time
    private bool isDragging = false;   // Flag to track if dragging
    SpriteRenderer sprite;
    Color color;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        color=sprite.color;
    }
    void OnMouseDown()
    {

        // Calculate the offset between the mouse position and the platform's position
        offset =(Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)-(Vector2)transform.position;
        isDragging = true; // Start dragging
        dragTimer = 0f;    // Reset drag timer
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
            dragTimer += Time.deltaTime;
            if(dragTimer > 3f)
            {
                color.a -= 0.01f;
                sprite.color = color;
                if(color.a<=0.1f)
                Destroy(gameObject);
            }
        }
        
    }
    void OnMouseUp()
    {
        // Stop dragging and reset timer
        isDragging = false;
        dragTimer = 0f;
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

 

}
