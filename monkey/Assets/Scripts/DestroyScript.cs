using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    public GameObject player;
    public GameObject white_platformprefab;
    public GameObject bouncy_platformprefab;
    public GameObject move_platformprefab;
    public GameObject break_platformprefab;

    public GameObject hat;
    public GameObject jet;
    [SerializeField] private GameObject[] enemyPrefabs;

    private int random;
    //private int platformNum = 10;
    public static bool isEquipped;
    private void Start()
    {
        isEquipped = false;
    }

    private void Update()
    {
        random = Random.Range(1, 7);
        if(isEquipped)
        {
            DestroyAllEnemies();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("platform") || collision.CompareTag("bouncePlatform") ||
            collision.CompareTag("movePlatform"))
        {
            if (!isEquipped)
            {
                SpawnPlatforms(collision);
                Destroy(collision.gameObject);

            }
        }

        if (collision.CompareTag("hat") || collision.CompareTag("jetpack"))
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            EquipItem();
        }

        if (collision.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void EquipItem()
    {
        isEquipped=true;
        StartCoroutine(ResetEquipAfterTime(4f));
    }

    private IEnumerator ResetEquipAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        isEquipped = false;
        StartCoroutine(spawnEnemy());
    }

    private void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        Debug.Log("détroy");
    }

    private void SpawnWhitePlatformWithHat()
    {
        Vector2 platformPosition = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (1.5f + Random.Range(0f, 0.5f)));
        if(Random.Range(1,3) == 1)
        {
            Instantiate(white_platformprefab, platformPosition, Quaternion.identity);
            if (!isEquipped)
            {
                if (Random.Range(1, 10) == 1)
                {
                    Instantiate(hat, new Vector2(platformPosition.x, platformPosition.y + 1f), Quaternion.identity);
                }
                else if (Random.Range(1, 20) == 1)
                {
                    Instantiate(jet, new Vector2(platformPosition.x, platformPosition.y + 1f), Quaternion.identity);
                }
            }
            else { }
        }
        else
        {
            Instantiate(move_platformprefab, platformPosition, Quaternion.identity);
        }
    }
    private void SpawnPlatforms(Collider2D collision)
    {
        Vector2 platformPosition = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (1.5f + Random.Range(0f, 0.5f)));
        switch (random)
        {
            case 1:
            case 3:   
                Instantiate(white_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (1.5f + Random.Range(0f, 0.5f))), Quaternion.identity);
                break;
            case 4:
            case 6:
                SpawnWhitePlatformWithHat();
                break;
            case 2:
                //case 5:
                Instantiate(bouncy_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (1.5f + Random.Range(0f, 0.5f))), Quaternion.identity);
                break;
            case 5:
                Instantiate(white_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (1.5f + Random.Range(0f, 0.5f))), Quaternion.identity);
                if (!isEquipped)
                {
                    StartCoroutine(spawnEnemy());
                }
                else
                { 
                
                }
                break;
            default:
                collision.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (1.5f + Random.Range(0f, 0.5f)));
                break;
        }
    }
    private IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(5f);
        SpawnEnemy2();
    }
    private void SpawnEnemy2()
    {
        Vector2 spawnPosition;
        float verticalOffset;

        // Randomly decide whether to spawn above or below the platform
        bool spawnAbove = Random.value > 0.5f; // 50% chance to spawn above or below

        // Generate a random spawn position
        float xPosition = Random.Range(-3f, 3f);
        float baseYPosition = player.transform.position.y + (15 + Random.Range(0.1f, 0.5f));

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
        Debug.Log("Enemy spawned!");
    }

}
