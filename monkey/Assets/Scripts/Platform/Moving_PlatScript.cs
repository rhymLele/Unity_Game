using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_PlatScript : MonoBehaviour
{
    public float speed = 2.0f;
    private float leftLimit = -6f;
    private float rightLimit = 6f;
    private bool movingRight = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {

            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
            }
        }
    }
}