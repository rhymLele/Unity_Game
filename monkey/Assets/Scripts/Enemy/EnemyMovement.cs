using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float moveChance = 0.5f; // 50% chance to move

    private Vector2 startingPosition;
    private int moveDirection = 1; // 1 for right, -1 for left
    private bool canMove; // Determines if the enemy can move

    void Start()
    {
        startingPosition = transform.position;

        // Determine if this enemy can move based on the moveChance
        canMove = Random.value < moveChance;
    }

    void Update()
    {
        if (canMove)
        {
            // Move the enemy horizontally
            transform.Translate(Vector2.right * moveSpeed * moveDirection * Time.deltaTime);

            // Check if the enemy has reached the move distance limit
            if (Vector2.Distance(startingPosition, transform.position) >= moveDistance)
            {
                // Reverse direction
                moveDirection *= -1;
            }
        }
    }
}