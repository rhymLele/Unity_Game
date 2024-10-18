using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float moveInput;
    private float moveSpeed = 8f;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    private GameObject bulletInst;
    public GameObject hatPre;
    public GameObject jetPre;

    private bool isStarted = false;
    private float topScore = 0f;
    public Text scoreText;
    public Text startText;
    public Text gameOver;

    private bool gameEnded = false;

    public GameObject[] platforms;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;
        rb2d.velocity = Vector2.zero;
        scoreText.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isStarted)
            {
                StartGame();
            }
            else if (gameEnded)
            {
                //RestartGame();
            }
        }

        if (isStarted && !gameEnded)
        {
            HandleMovement();
            HandleShooting();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Get the bounds of the player and enemy
            Bounds playerBounds = GetComponent<Collider2D>().bounds;
            Bounds enemyBounds = collision.collider.bounds;

            // Check if player is above the enemy (player's feet touch the enemy's head)
            bool playerAboveEnemy = playerBounds.min.y > enemyBounds.max.y;

            // Check if player hit the enemy from the side or below (player dies)
            bool playerHitFromSideOrBelow = !playerAboveEnemy;

            if (playerAboveEnemy)
            {
                // Player kills enemy by jumping on top
                Destroy(collision.gameObject);
                //topScore += 10;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);// Reset vertical velocity to avoid stacking forces
                rb2d.AddForce(new Vector2(0, 800f));// Add upward force (adjust value for jump height)
                //rb2d.AddForce(Vector3.up * 600f);
                Debug.Log("Enemy killed.");
            }
            else if (playerHitFromSideOrBelow)
            {
                // Player dies if hit from the side or below
                //Destroy(gameObject);
                //rb2d.AddForce(new Vector2(0, -500f));
                Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
                rb2d.gravityScale = 0f;
                rb2d.velocity = Vector2.zero;
                enemyRb.velocity = Vector2.zero;
                enemyRb.angularVelocity = 0f;
                
                EndGame();
                Debug.Log("Player died.");
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("hat"))
        {
            Debug.Log("Player touches hat");
            EquipHat(gameObject);
        }
        if (collision.gameObject.CompareTag("jetpack"))
        {
            EquipJet(gameObject);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            bulletInst=Instantiate(bullet,bulletSpawnPoint.position,bulletSpawnPoint.rotation);
        }
    }

    private void StartGame()
    {
        isStarted = true;
        startText.gameObject.SetActive(false);
        rb2d.gravityScale = 4f;
        scoreText.gameObject.SetActive(true);
    }

    private void HandleMovement()
    {
        if (moveInput < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (rb2d.velocity.y > 0 && transform.position.y > topScore)
        {
            topScore = transform.position.y;
        }

        scoreText.text = "Score: " + Mathf.Round(topScore).ToString();

        if (rb2d.velocity.y < 0 && transform.position.y < topScore - 30f)
        {
            EndGame();
        }
    }

    void EquipHat(GameObject player)
    {
        // Create the hat instance
        GameObject hate = Instantiate(hatPre, player.transform);

        // Set the local position relative to the player's position
        hate.transform.localPosition = new Vector3(0, 0.2f, 0); // Adjust Y value as necessary

        // Destroy the hat after the specified duration
        Destroy(hate, 1f); // Use 'hate' instead of 'hat'
    }

    void EquipJet(GameObject player)
    {
        GameObject getJet = Instantiate(jetPre, player.transform);

        getJet.transform.localPosition = new Vector3(0.2f, 0, 0);

        Destroy(getJet, 1f); 
    }

    void FixedUpdate()
    {
        if (isStarted && !gameEnded)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
        }
    }
    
    private void EndGame()
    {
        gameEnded = true;
        rb2d.gravityScale = 0f;
        rb2d.velocity = Vector2.zero;
        gameOver.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        gameOver.text = "Game Over! Score: " + Mathf.Round(topScore).ToString();
    }

    private void RestartGame()
    {
        gameEnded = false;
        topScore = 0f;
        rb2d.gravityScale = 0f;
        rb2d.velocity = Vector2.zero;

        transform.position = new Vector2(0, 0);

        //Instantiate(platforms[0], new Vector2(0, rb2d.position.y -2), Quaternion.identity);

        for (int i = 1; i < platforms.Length; i++)
        {
            //Instantiate(platforms[i], new Vector2(Random.Range(-3.5f, 3.5f),Random.Range(1f,3f)), Quaternion.identity);
        }

        scoreText.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
    }
}