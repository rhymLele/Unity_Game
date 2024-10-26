using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("General Bullet Stats")]
    [SerializeField] private float DestroyTime = 2.5f;

    [Header("Normal Bullet Stats")]
    [SerializeField] private float normalBulletSpeed = 20f;
    [SerializeField] private float normalBulletDamage = 1f;

    [Header("Physics Bullet Stats")]
    [SerializeField] private float physicsBulletSpeed = 17.5f;
    [SerializeField] private float physicsBulletGravity = 3f;
    [SerializeField] private float physicsBulletDamage = 2f;

    private Rigidbody2D rb;
    private AudioController audioController;
    private float damage;
    //
    public enum BulletType
    {
         Normal,
         Physics
    }
    public BulletType bulletType;
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
        SetRBStats();
        InitializeBulletStats();
    }

    private void SetRBStats()
    {
        if (bulletType == BulletType.Normal)
        {
            rb.gravityScale = 0f;
        }else if (bulletType == BulletType.Physics)
        {
            rb.gravityScale =physicsBulletGravity;
        }
    }
    private void FixedUpdate()
    {
        if(bulletType==BulletType.Physics)
        {
            transform.up = rb.velocity;
        }
    }
    private void SetDestroyTime()
    {
        Destroy(gameObject,DestroyTime);
    }

    private void SetStraightVelocity()
    {
        rb.velocity=transform.up*normalBulletSpeed;
    }
    private void SetPhysicsVelocity()
    {
        rb.velocity = transform.up * physicsBulletSpeed;
    }
    private void InitializeBulletStats()
    {
        if(bulletType==BulletType.Normal)
        {
            SetStraightVelocity();
            damage=normalBulletDamage;
        }
        else if(bulletType==BulletType.Physics)
        {
            SetPhysicsVelocity();
            damage=physicsBulletDamage;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetNormalBulletType()
    {
        bulletType = BulletType.Normal;
        InitializeBulletStats();
    }
    public void SetPhysicsBulletType()
    {
        bulletType = BulletType.Physics;
        InitializeBulletStats(); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an enemy
        if (other.CompareTag("enemy"))
        {

            IDamageable iDam=other.gameObject.GetComponent<IDamageable>();
            if(iDam != null)
            {
                iDam.Damgage(damage);   
            }

            // Destroy the enemy GameObject
            Destroy(other.gameObject,0f);

            // Destroy the bullet itself
            Destroy(gameObject);
        }
    }
}
