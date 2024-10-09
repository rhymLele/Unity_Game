using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2d;
    private float moveInput;
    private float moveSpeed=5f;

    private bool isStarted = false;

    private float topScore = 0f;
    public Text scoreText;
    public Text startText;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.gravityScale = 0f;
        rb2d.velocity = Vector2.zero;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isStarted == false)
        {
            isStarted = true;
            startText.gameObject.SetActive(false);
            rb2d.gravityScale = 4f;

        }
        if(isStarted == true)
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
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStarted == true)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
        }
    }
}
