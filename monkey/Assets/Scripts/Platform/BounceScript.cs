using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    private AudioController audioController;
    void Start()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !DestroyScript.isEquipped)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (rb.velocity.y <= 0)
                {
                    if (audioController.loxoClip != null)
                    {
                        audioController.PlaySFX(audioController.loxoClip);
                    }
                    else
                    {
                        Debug.LogError("jumpClip chưa được gán trong AudioController!");
                    }
                    rb.AddForce(Vector2.up * 600f);
                }
            }
        }
    }
}
