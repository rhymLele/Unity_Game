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
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Toggle the direction each time the player touches the platform
            moveRight = !moveRight;
            // Start moving the platform
            StartCoroutine(MovePlat());
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
