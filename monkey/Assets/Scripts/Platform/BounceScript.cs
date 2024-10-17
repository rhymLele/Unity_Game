using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    private AudioController audioController;

    // Start is called before the first frame update
    void Start()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Kt khi rơi chạm plat
            if (rb.velocity.y <= 0)
            {
                // Phát âm thanh jumpClip khi va chạm
                if (audioController.loxoClip != null)
                {
                    audioController.PlaySFX(audioController.loxoClip);
                }
                else
                {
                    Debug.LogError("jumpClip chưa được gán trong AudioController!");
                }
                rb.AddForce(Vector2.up * 800f);
            }
        }
    }
}
