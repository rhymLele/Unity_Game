using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    public void Damgage(float damageAccount)
    {
        currentHealth-=damageAccount;
        if(currentHealth<= 0)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = Random.Range(1, 2);
        currentHealth =maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
