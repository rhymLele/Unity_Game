using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Vector2 offset;
    private float dragTimer = 0f;
    private bool isDragging = false;
    SpriteRenderer sprite;
    Color color;
    private AudioController audioController;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        color=sprite.color;
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }
    void OnMouseDown()
    {
        offset =(Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)-(Vector2)transform.position;
        isDragging = true;
        dragTimer = 0f;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
            dragTimer += Time.deltaTime;
            if(dragTimer > 3f)
            {
                color.a -= 0.01f;
                sprite.color = color;
                if(color.a<=0.1f)
                Destroy(gameObject);
            }
        }
        
    }
    void OnMouseUp()
    {
        isDragging = false;
        dragTimer = 0f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !DestroyScript.isEquipped)
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                if (playerRb.velocity.y <= 0)
                {
                    playerRb.AddForce(Vector3.up * 400f);
                    if (audioController.jumpClip != null)
                    {
                        audioController.PlaySFX(audioController.jumpClip);
                    }
                }
            }
        }

    }

 

}
