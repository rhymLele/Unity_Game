using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode2Platform : MonoBehaviour
{
    public GameObject player;
    public GameObject offset_platformprefab;
    private int random;
    //private int platformNum = 10;

    private void Start()
    {

    }

    private void Update()
    {
        random = Random.Range(1, 7);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("o_plat"))
        {

            SpawnPlatforms(collision);
            Destroy(collision.gameObject);


        }
    }

    private void SpawnWhitePlatformWithHat()
    {
        Vector2 platformPosition = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (3.5f + Random.Range(0f, 0.5f)));
        if (Random.Range(1, 3) == 1)
        {
            Instantiate(offset_platformprefab, platformPosition, Quaternion.identity);


        }
        //else
        //{
        //    Instantiate(offset_platformprefab, platformPosition, Quaternion.identity);
        //}
    }
    private void SpawnPlatforms(Collider2D collision)
    {
        Vector2 platformPosition = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (3.5f + Random.Range(0f, 0.5f)));
        switch (random)
        {
            case 1:
            case 3:
            case 4:
            case 6:
            case 2:
            case 5:
                Instantiate(offset_platformprefab, new Vector2(Random.Range(-6f, 6f), player.transform.position.y + (3.5f + Random.Range(0.5f, 1f))), Quaternion.identity);
                break;
            default:
                collision.transform.position = new Vector2(Random.Range(-6f, 6f), player.transform.position.y + (3.5f + Random.Range(0f, 0.5f)));
                break;
        }
    }

}
