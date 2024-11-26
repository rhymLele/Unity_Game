using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    private Collider2D playerCollider;
    private Rigidbody2D rb2d;
    private float moveInput;
    private float moveSpeed = 8f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite shootSprite;
    private SpriteRenderer spriteRenderer;
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
    GameObject getJet;

    private BackgroundManager backgroundManager;

    public GameObject[] platforms;
    private AudioController audioController;
    private bool playerStatus;

    private int leftRight;
    private float timer=0;

    private void Start()
    {
        Initialized();
    }
    private void FixedUpdate()
    {
        if (isStarted && playerStatus)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
            if (getJet != null) UpdateJetPosition(getJet);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerStatus==true)
            {
                StartGame();
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

    private void Initialized()
    {
        playerStatus = true;
        playerCollider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;
        rb2d.velocity = Vector2.zero;
        scoreText.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        isEquipped = false;

        backgroundManager = FindObjectOfType<BackgroundManager>();
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
            if (timer > 0.3)
            {
                timer = 0;
                bulletInst = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

                Sprite shootSprite = backgroundManager.GetPlayerShootSprite();
                if (shootSprite != null)
                    bulletInst = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                {
                    GetComponent<SpriteRenderer>().sprite = shootSprite;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            Sprite normalSprite = backgroundManager.GetPlayerNormalSprite();
            if (normalSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = normalSprite;
            }
        }
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

    private void StartGame()
    {
        isStarted = true;
        playerStatus = true;
        startText.gameObject.SetActive(false);
        rb2d.gravityScale = 4f;
        scoreText.gameObject.SetActive(true);
    }

    private void EndGame()
    {
        playerStatus = false;
        rb2d.gravityScale = 0f;
        rb2d.velocity = Vector2.zero;
        scoreText.gameObject.SetActive(false);

        PlayerPrefs.SetFloat("CurrentScore", topScore);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Endgame");
    }

    void EquipHat(GameObject player)
    {
        rb2d.gravityScale = 6f;
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
        getJet = Instantiate(jetPre, player.transform);
        UpdateJetPosition(getJet); // Đặt vị trí ban đầu
        Destroy(getJet, 1.1f);
        isEquipped = false;
        StartCoroutine(ReactivatePlayer(1.1f));
    }
    void UpdateJetPosition(GameObject Jret)
    {
        float offsetX = 0.2f;
        if (Jret != null)
        {
            if (leftRight == -1)
            {
                Jret.transform.localPosition = new Vector3(-offsetX, 0, 0);
            }
            else
            {
                Jret.transform.localPosition = new Vector3(offsetX, 0, 0);
            }
        }
    }
    
    private IEnumerator ReactivatePlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerCollider.enabled = true;
    }

}