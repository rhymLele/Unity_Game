using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private float highScore = 0f;
    public Text scoreText;
    public Text startText;
    public Text gameOver;
    private bool isEquipped;
    //private bool gameEnded = false;

    public GameObject[] platforms;
    private AudioController audioController;
    private bool playerStatus;

    private int leftRight;

    void Start()
    {
        playerStatus = true;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;
        rb2d.velocity = Vector2.zero;
        scoreText.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        isEquipped = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerStatus==true)
            {
                StartGame();
            }
            else if (playerStatus==false)
            {
                RestartGame();
            }

        }
        if (playerStatus&&isStarted)
        {
            HandleMovement();
            HandleShooting();
        }
    }
    private void RestartGame()
    {
        // Reload the scene to reset everything
        SceneManager.LoadScene("NightScene");
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
            if(!isEquipped)
            {
                EquipHat(gameObject);
            }
            
        }
        if (collision.gameObject.CompareTag("jetpack"))
        {
            if (!isEquipped)
            { EquipJet(gameObject); }
            
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
        playerStatus = true;
        startText.gameObject.SetActive(false);
        rb2d.gravityScale = 4f;
        scoreText.gameObject.SetActive(true);
        //audioController.OnOffMusicBackground();
    }

    private void HandleMovement()
    {
        if (moveInput < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            leftRight = 1;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
            leftRight = -1;
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
        isEquipped = false;
    }

    void EquipJet(GameObject player)
    {
        GameObject getJet = Instantiate(jetPre, player.transform);
        float offsetX = 0.2f;
        if (leftRight ==-1) // Moving left
        {
            getJet.transform.localPosition = new Vector3(-offsetX, 0, 0);
        }
        else // Moving right or stationary
        {
            getJet.transform.localPosition = new Vector3(offsetX, 0, 0);
        }

        Destroy(getJet, 1.1f);
        isEquipped = false;
    }

    void FixedUpdate()
    {
        if (isStarted && playerStatus)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
        }
    }
    
    private void EndGame()
    {
        //gameEnded = true;
        playerStatus = false;
        rb2d.gravityScale = 0f;
        rb2d.velocity = Vector2.zero;
       // gameOver.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

        PlayerPrefs.SetFloat("CurrentScore", topScore);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Endgame");
        /*gameOver.text = "Game Over! Score: " + Mathf.Round(topScore).ToString();
        if (topScore > highScore)
        {
            highScore = topScore;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
            gameOver.text += "\nNew High Score!";
        }
        else
        {
            gameOver.text += "\nHigh Score: " + Mathf.Round(highScore).ToString();
        }


        audioController.OnOffMusicBackground();
        if (audioController != null && audioController.gameoverClip != null)
        {
            audioController.PlaySFX(audioController.gameoverClip);
        }
        else
        {
            Debug.LogError("GameOverClip chưa được gán trong AudioController!");
        }*/
    }
}