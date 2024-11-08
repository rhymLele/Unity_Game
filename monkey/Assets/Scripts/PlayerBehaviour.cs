using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerBehaviour : MonoBehaviour
{
    private Collider2D playerCollider;
    private Rigidbody2D rb2d;
    private float moveInput;
    private float moveSpeed = 8f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    private bool isShooting;
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

    private int state;
    private string STATE_ANIMATION = "state";
    private Animator animator;
    private bool isJumping;

    void Start()
    {
    
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
        playerStatus = true;
        state = 2;
        rb2d.gravityScale = 0f;
        rb2d.velocity = Vector2.zero;
        scoreText.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
       
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        isEquipped = false;
        isShooting= false;
        isJumping = false;
    }
    void playAnimator()
    {
        if(isShooting)
        {
            state = 1;
        }else if(isJumping)
        {
            state=2;
        }
        animator.SetInteger(STATE_ANIMATION, state);    
    }
    private void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerStatus==true)
            {
                StartGame();
                isJumping = true;
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
            if (!isEquipped)
            {
                rb2d.gravityScale = 4f;
            }
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
            Bounds playerBounds = GetComponent<Collider2D>().bounds;
            Bounds enemyBounds = collision.collider.bounds;

            bool playerAboveEnemy = playerBounds.min.y > enemyBounds.max.y;

            bool playerHitFromSideOrBelow = !playerAboveEnemy;

            if (playerAboveEnemy)
            {
                Destroy(collision.gameObject);
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(new Vector2(0, 800f));
                Debug.Log("Enemy killed.");
            }
            else if (playerHitFromSideOrBelow)
            {
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
            if (!isEquipped)
            {
                EquipHat(gameObject);
                
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("jetpack"))
        {
            Debug.Log("Player touches jetpack");
            if (!isEquipped)
            { EquipJet(gameObject); }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }

        private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            isShooting = true;
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

        if (rb2d.velocity.y < 0 && transform.position.y < topScore - 75f)
        {
            EndGame();
        }
    }

    void EquipHat(GameObject player)
    {
        rb2d.gravityScale = 2f;
        playerCollider.enabled = false;
        GameObject hate = Instantiate(hatPre, player.transform);
        hate.transform.localPosition = new Vector3(0, 0.2f, 0);
        Destroy(hate, 1f);
        isEquipped = false;
        StartCoroutine(ReactivatePlayer(1f));
    }
    
    void EquipJet(GameObject player)
    {
        rb2d.gravityScale = 7f;
        playerCollider.enabled = false;
        GameObject getJet = Instantiate(jetPre, player.transform);
        float offsetX = 0.2f;
        if (leftRight == -1)
        {
            getJet.transform.localPosition = new Vector3(-offsetX, 0, 0);
        }
        else
        {
            getJet.transform.localPosition = new Vector3(offsetX, 0, 0);
        }
        Destroy(getJet, 1.1f);
        isEquipped = false;
        StartCoroutine(ReactivatePlayer(1.1f));
    }

    void FixedUpdate()
    {
        if (isStarted && playerStatus)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
        }
        //playAnimator();
    }

    private IEnumerator ReactivatePlayer(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        playerCollider.enabled = true;
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
    }
}