using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject platformPrefab;
    private GameObject newPlat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        newPlat=(GameObject) Instantiate(platformPrefab,
            new Vector2(Random.Range(-5.5f,5.5f)
            ,player.transform.position.y
            +(14+Random.Range(0.5f,1f))),Quaternion.identity);
        Destroy(collision.gameObject);
    }
}
