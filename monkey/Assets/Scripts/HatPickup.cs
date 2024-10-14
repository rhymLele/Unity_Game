using System.Collections;
using UnityEngine;

public class HatPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                FlyUp(playerRb);
            }
            Destroy(gameObject);
        }
    }
    private void FlyUp(Rigidbody2D obj)
    {
        obj.AddForce(Vector3.up * 1200f);
    }
}