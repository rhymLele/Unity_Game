using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    public GameObject player;
    public GameObject white_platformprefab;
    public GameObject bouncy_platformprefab;
    public GameObject move_platformprefab;
    public GameObject hat;
    public GameObject jet;
    [SerializeField] private GameObject[] enemyPrefabs;
    private void Start()
    {
        //SpawnInitialEnemy(1);
    }

    private void SpawnInitialEnemy(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy2();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Platform
        #region
        if (collision.CompareTag("platform"))
        {
            if (Random.Range(1, 10) == 1)
            {
                Destroy(collision.gameObject);
                SpawnEnemy2();
            }
            else if (Random.Range(1, 18) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(bouncy_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (3 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else if (Random.Range(1, 10) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(move_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (3 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else
            {
                collision.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (3 + Random.Range(0.2f, 1f)));
            }
        }
        #endregion

        //Bounce
        #region
        else if (collision.CompareTag("bouncePlatform"))
        {
            if (Random.Range(1, 6) == 1)
            {
                SpawnWhitePlatformWithHat();
                Destroy(collision.gameObject);
            }
            else if (Random.Range(1, 10) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(move_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (3 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else
            {
                collision.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (3 + Random.Range(0.2f, 1f)));
            }
        }
        #endregion

        //Move
        #region
        else if (collision.CompareTag("movePlatform"))
        {
            if (Random.Range(1, 6) == 1)
            {
                SpawnWhitePlatformWithHat();
                Destroy(collision.gameObject);
            }
            else if (Random.Range(1, 18) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(bouncy_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (3 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else
            {
                collision.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (3 + Random.Range(0.2f, 1f)));
            }
        }
        #endregion

        //Hat
        #region
        else if (collision.CompareTag("hat"))
        {
            Destroy(collision.gameObject);
        }
        #endregion

        //Jet
        #region
        else if (collision.CompareTag("jetpack"))
        {
            Destroy(collision.gameObject);
        }
        #endregion

        //Enemy
        #region
        else if (collision.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
        }
        
        #endregion
    }
    private void SpawnWhitePlatformWithHat()
    {
        Vector2 platformPosition = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (3 + Random.Range(0.2f, 1f)));
        Instantiate(white_platformprefab, platformPosition, Quaternion.identity);

        if (Random.Range(1, 8) == 1)
        {
            Instantiate(hat, new Vector2(platformPosition.x, platformPosition.y + 1f), Quaternion.identity);
        }
        else if (Random.Range(1, 20) == 1)
        {
            Instantiate(jet, new Vector2(platformPosition.x, platformPosition.y + 1f), Quaternion.identity);
        }

    }
    private void SpawnEnemy()
    {
        // Spawn the enemy at a random position above the player
        Vector2 enemyPosition = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f)));
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject randomEnemyPrefab = enemyPrefabs[randomIndex];
        Instantiate(randomEnemyPrefab, enemyPosition, Quaternion.identity);
    }
    private void SpawnEnemy2()
    {
        Vector2 spawnPosition;
        float verticalOffset;

        // Randomly decide whether to spawn above or below the platform
        bool spawnAbove = Random.value > 0.5f; // 50% chance to spawn above or below

        // Generate a random spawn position
        float xPosition = Random.Range(-3.5f, 3.5f);
        float baseYPosition = player.transform.position.y + (5 + Random.Range(0.2f, 1f));

        // Determine vertical offset
        if (spawnAbove)
        {
            verticalOffset = Random.Range(1f, 3f); // Spawn 1 to 3 units above
            spawnPosition = new Vector2(xPosition, baseYPosition + verticalOffset);
        }
        else
        {
            verticalOffset = Random.Range(1f, 3f); // Spawn 1 to 3 units below
            spawnPosition = new Vector2(xPosition, baseYPosition - verticalOffset);
        }

        // Ensure the enemy is not spawning too low (e.g., below ground level)
        if (spawnPosition.y < 0) // Adjust this condition based on your ground level
        {
            spawnPosition.y = 0; // Clamp to ground level if below
        }

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject randomEnemyPrefab = enemyPrefabs[randomIndex];
        Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
        Vector2 platformPosition = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (3 + Random.Range(0.2f, 1f)));
        Instantiate(white_platformprefab, platformPosition, Quaternion.identity);
    }

}