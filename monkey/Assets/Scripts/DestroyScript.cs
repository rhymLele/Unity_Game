using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    public GameObject player;
    public GameObject white_platformprefab;
    public GameObject bouncy_platformprefab;
    public GameObject move_platformprefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("platform"))
        {
            if (Random.Range(1, 10) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(bouncy_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else if(Random.Range(1, 15) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(move_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else
            {
                collision.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f)));
            }
        }
        else if (collision.CompareTag("bouncePlatform"))
        {
            if (Random.Range(1, 6) == 1)
            {
                Instantiate(white_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
                Destroy(collision.gameObject);
            }
            else if (Random.Range(1, 15) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(move_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else
            {
                collision.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f)));
            }
        }
        else if (collision.CompareTag("movePlatform"))
        {
            if (Random.Range(1, 6) == 1)
            {
                Instantiate(white_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
                Destroy(collision.gameObject);
            }
            else if (Random.Range(1, 10) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(bouncy_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else
            {
                collision.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f)));
            }
        }
    }
}