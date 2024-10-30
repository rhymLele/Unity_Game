using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float normalBulletSpeed = 20f;
    [SerializeField] private float physicsBulletSpeed = 14.5f;
    [SerializeField] private float physicsDamage = 2f;
    [SerializeField] private float normalDamage= 1;
    [SerializeField] private float DestroyTime = 2.5f;
    private float damage;
    private Rigidbody2D rb;
    private AudioController audioController;
    public enum bulletType
    {
        Normal,Physics
    }
    public bulletType type;
    private void initialBulletStats()
    {
        if(type == bulletType.Physics)
        {
            SetPhysicVelocity();
            damage=physicsDamage;
        }
        else if(type == bulletType.Normal)
        {
            SetStraightVelocity();damage = normalDamage;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
        if (audioController != null && audioController.bandanClip != null)
        {
            audioController.PlaySFX(audioController.bandanClip);
        }
        else
        {
            Debug.LogError("bandanClip chưa được gán trong AudioController!");
        }
        SetDestroyTime();
        setRBSStats();
        initialBulletStats();
        
        //SetStraightVelocity();
    }
    private void FixedUpdate()
    {
        if (type == bulletType.Physics)
        {
            transform.up = rb.velocity;
        }
    }
    private void setRBSStats()
    {
        if (type == bulletType.Physics)
        {
            rb.gravityScale = 4f;
        }
        else if (type == bulletType.Normal)
        {
            rb.gravityScale = 0f;
        }
    }
    private void SetDestroyTime()
    {
        Destroy(gameObject,DestroyTime);
    }
    private void SetPhysicVelocity()
    {
        rb.velocity = transform.up*physicsBulletSpeed;
    }
    private void SetStraightVelocity()
    {
        rb.velocity=transform.up*normalBulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an enemy
        if (other.CompareTag("enemy"))
        {
            IDamgable iDam=other.gameObject.GetComponent<IDamgable>();
            if(iDam != null)
            {
                iDam.Damage(damage);
            }

            // Destroy the enemy GameObject
            //Destroy(other.gameObject,0f);

            // Destroy the bullet itself
            Destroy(gameObject);
        }
    }
}
