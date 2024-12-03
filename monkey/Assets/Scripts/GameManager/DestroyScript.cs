using System.Collections;
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
    public static bool isEquipped;
    public static float limitSpawn = 7f;

    public static int currentEnemyCount = 0;
    private const int maxEnemyCount = 5;

    private void Start()
    {
        isEquipped = false;
        currentEnemyCount = 0;
    }

    private void Update()
    {
        random = Random.Range(1, 7);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("platform") || collision.CompareTag("bouncePlatform") ||
            collision.CompareTag("movePlatform"))
        {
            SpawnPlatforms(collision);
            Destroy(collision.gameObject);
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
            currentEnemyCount--;
        }
    }

    public void EquipItem()
    {
        isEquipped=true;
        StartCoroutine(ResetEquipAfterTime(1f));
    }

    private IEnumerator ResetEquipAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        isEquipped = false;
        StartCoroutine(spawnEnemy());
    }

    private void SpawnWhitePlatformWithHat()
    {
        Vector2 platformPosition = new Vector2(Random.Range(-limitSpawn /2, limitSpawn /2), player.transform.position.y + (3.5f + Random.Range(0f, 0.5f)));
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
        Vector2 platformPosition = new Vector2(Random.Range(-limitSpawn, limitSpawn), player.transform.position.y + (3.5f + Random.Range(0f, 0.5f)));
        switch (random)
        {
            case 1:
            case 3:
                Instantiate(white_platformprefab, new Vector2(Random.Range(-limitSpawn, limitSpawn), player.transform.position.y + (3.5f + Random.Range(0f, 0.5f))), Quaternion.identity);
                break;
            case 4:
            case 6:
                SpawnWhitePlatformWithHat();
                break;
            case 2:
                //case 5:
                Instantiate(bouncy_platformprefab, new Vector2(Random.Range(-limitSpawn, limitSpawn), player.transform.position.y + (3.5f + Random.Range(0f, 0.5f))), Quaternion.identity);
                break;
            case 5:
                Instantiate(white_platformprefab, new Vector2(Random.Range(-limitSpawn, limitSpawn), player.transform.position.y + (3.5f + Random.Range(0f, 0.5f))), Quaternion.identity);
                if (!isEquipped)
                {
                    StartCoroutine(spawnEnemy());
                }
                break;
            default:
                collision.transform.position = new Vector2(Random.Range(-limitSpawn, limitSpawn), player.transform.position.y + (3.5f + Random.Range(0f, 0.5f)));
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
        if (currentEnemyCount >= maxEnemyCount)
        {
            return;
        }
        else
        {
            Vector2 spawnPosition;
            float verticalOffset;
            bool spawnAbove = Random.value > 0.5f;
            float xPosition = Random.Range(-limitSpawn/2, limitSpawn/2);
            float baseYPosition = player.transform.position.y + (15 + Random.Range(0.1f, 0.5f));

            if (spawnAbove)
            {
                verticalOffset = Random.Range(1f, 3f);
                spawnPosition = new Vector2(xPosition, baseYPosition + verticalOffset);
            }
            else
            {
                verticalOffset = Random.Range(1f, 3f);
                spawnPosition = new Vector2(xPosition, baseYPosition - verticalOffset);
            }

            if (spawnPosition.y < 0)
            {
                spawnPosition.y = 0;
            }

            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject randomEnemyPrefab = enemyPrefabs[randomIndex];
            GameObject enemy = Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);

            currentEnemyCount++;
            Debug.Log("Enemy" + currentEnemyCount + "spawned!");

        }
    }

}
