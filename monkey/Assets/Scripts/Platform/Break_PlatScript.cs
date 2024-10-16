using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_PlatScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer của platform
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            if (playerRb.velocity.y <= 0)
            {
                playerRb.AddForce(Vector3.up * 500f);
                StartCoroutine(FadeOutAndDestroy()); // Bắt đầu coroutine để mờ dần và biến mất
            }
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        Color color = spriteRenderer.color; // Lấy màu hiện tại của platform
        float fadeDuration = 0.5f; // Thời gian mờ dần

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            color.a = Mathf.Lerp(1, 0, normalizedTime); // Giảm alpha từ 1 đến 0
            spriteRenderer.color = color; // Cập nhật màu cho platform
            yield return null; // Chờ đến frame tiếp theo
        }

        Destroy(gameObject); // Biến mất platform sau khi mờ dần
    }
}