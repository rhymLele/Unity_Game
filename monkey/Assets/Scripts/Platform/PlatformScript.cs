using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            if (playerRb.velocity.y <= 0)
            {
                playerRb.AddForce(Vector3.up * 500f);
            }
            /*else
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            } */
        }
    }
}
