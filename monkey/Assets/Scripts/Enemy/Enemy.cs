using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamgable
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    public void Damage(int damage)
    {
       currentHealth-=damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = Random.Range(1, 3);
        currentHealth=maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
