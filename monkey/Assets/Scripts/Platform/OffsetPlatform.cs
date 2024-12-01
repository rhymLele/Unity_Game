using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class OffsetPlatform : MonoBehaviour
{
    private Vector2 offset;
    [SerializeField] private float moveDistance = 3.0f;
    [SerializeField] private float speed = 2.5f;          
    private bool moveRight = true;    
    [SerializeField] private float minX = -6.5f;       
    [SerializeField] private float maxX = 6.5f;
    [SerializeField] private float moveChance = 0.5f;

    private bool isMoving;
    private AudioController audioController;
    void Start()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
        if (isMoving = Random.value < moveChance)
        {
            moveRight = false;
        }
        else
        {
            moveRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveRight = !moveRight;
            StartCoroutine(MovePlat());
        }
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

    private IEnumerator MovePlat()
    {
        Vector3 targetPosition;

        if (moveRight)
        {
            targetPosition = transform.position + new Vector3(moveDistance, 0, 0); 
        }
        else
        {
            targetPosition = transform.position - new Vector3(moveDistance, 0, 0); 
        }

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            moveRight = !moveRight;
            StartCoroutine(MovePlat()); 
        }
    }

}
