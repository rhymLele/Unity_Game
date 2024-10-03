using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2d;
    private float moveInput;
    private float moveSpeed=5f;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb2d.velocity=new Vector2 (moveInput* moveSpeed, rb2d.velocity.y);
    }
}
