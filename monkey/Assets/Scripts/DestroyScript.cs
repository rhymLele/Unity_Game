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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Platform
        #region
        if (collision.CompareTag("platform"))
        {
            if (Random.Range(1, 18) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(bouncy_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else if (Random.Range(1, 10) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(move_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else
            {
                collision.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f)));
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
                Instantiate(move_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else
            {
                collision.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f)));
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
                Instantiate(bouncy_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
            }
            else
            {
                collision.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f)));
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
    }

    private void SpawnWhitePlatformWithHat()
    {
        Vector2 platformPosition = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f)));
        Instantiate(white_platformprefab, platformPosition, Quaternion.identity);

        if (Random.Range(1, 8) == 1)
        {
            Instantiate(hat, new Vector2(platformPosition.x, platformPosition.y + 1f), Quaternion.identity);
        }
    }
}