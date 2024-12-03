using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IDamgable
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 0.2f;
    [SerializeField] private float moveChance = 0.5f;
    private AudioController audioController;
    private Vector2 startingPosition;
    private int moveDirection = 1;
    private bool canMove;
    private int currentHealth;

    private Rigidbody2D enemybd;
    private Collider2D enemycl;

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            enemybd.gravityScale = 5f;
            enemybd.velocity = Vector2.zero;
            enemybd.AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
            enemycl.enabled = false;
            if(DestroyScript.currentEnemyCount >= 0)
            {
                DestroyScript.currentEnemyCount--;
            }
            StartCoroutine(DestroyAfterDelay(1.5f));
        }
    }

    void Start()
    {
        startingPosition = transform.position;
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();

        enemybd = GetComponent<Rigidbody2D>();
        enemybd.gravityScale = 0f;
        enemybd.velocity = Vector2.zero;
        enemycl = GetComponent<Collider2D>();
        enemycl.enabled = true;

        canMove = Random.value < moveChance;

        if (audioController != null && audioController.quaiClip != null)
        {
            audioController.PlaySFX(audioController.quaiClip);
        }

        currentHealth = Random.Range(1, 3);
    }

    void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector2.right * moveSpeed * moveDirection * Time.deltaTime);

            if (Vector2.Distance(startingPosition, transform.position) >= moveDistance)
            {
                moveDirection *= -1;
            }
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}