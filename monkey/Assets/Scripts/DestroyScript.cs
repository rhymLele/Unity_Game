using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{

    public GameObject player;
    public GameObject white_platformprefab;
    public GameObject bouncy_platformprefab;
    private GameObject myPlat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("w_plat"))
        {
            if (Random.Range(1, 9) == 1)
            {
                Destroy(collision.gameObject);
                Instantiate(bouncy_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
                
            }
            else
            {
                collision.gameObject.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f)));
            }
            
        }
        else if (collision.gameObject.name.StartsWith("w_b"))
        {
            if (Random.Range(1, 6) == 1)
            {
                Instantiate(white_platformprefab, new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f))), Quaternion.identity);
                Destroy(collision.gameObject);
            }
            else
            {
                collision.gameObject.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), player.transform.position.y + (5 + Random.Range(0.2f, 1f)));
            }
        }

    }
}
