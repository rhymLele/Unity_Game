using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    private AudioController audioController;
    // Start is called before the first frame update
    void Start()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            if (playerRb.velocity.y <= 0)
            {
                playerRb.AddForce(Vector3.up * 600f);
                if (audioController.jumpClip != null)
                {
                    audioController.PlaySFX(audioController.jumpClip);
                }
                else
                {
                    Debug.LogError("jumpClip chưa được gán trong AudioController!");
                }
            }
            /*else
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            } */
        }
    }
}
