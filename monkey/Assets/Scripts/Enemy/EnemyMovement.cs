using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour,IDamgable
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 0.8f;
    [SerializeField] private float moveChance = 0.5f; // 50% chance to move
    private AudioController audioController;
    private Vector2 startingPosition;
    private int moveDirection = 1; // 1 for right, -1 for left
    private bool canMove; // Determines if the enemy can move
    private float currentHealth;

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject,1f);
        }
    }

    void Start()
    {
        startingPosition = transform.position;
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();

        // Determine if this enemy can move based on the moveChance
        canMove = Random.value < moveChance;
        if (audioController != null && audioController.quaiClip != null)
        {
            audioController.PlaySFX(audioController.quaiClip);
        }
        else
        {
            //Debug.LogError("quaiClip chưa được gán trong AudioController!");
        }
        currentHealth = Random.Range(1f, 3f);
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