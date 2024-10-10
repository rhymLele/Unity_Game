using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float moveInput;
    private float moveSpeed = 5f;

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

        if (rb2d.velocity.y < 0 && transform.position.y < topScore - 20f)
        {
            EndGame();
        }
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