using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS : MonoBehaviour
{
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
        if (collision.gameObject.tag == "leftWall")
        {
            transform.position = new Vector2(transform.position.x*-1-1, transform.position.y);
        }
        if (collision.gameObject.tag == "rightWall")
        {
            transform.position = new Vector2(-transform.position.x+1, transform.position.y);
        }
    }
}
