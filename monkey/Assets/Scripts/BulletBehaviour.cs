using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletBehaviour;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float normalBulletSpeed = 20f;
    [SerializeField] private float physicsBulletSpeed = 14.5f;
    [SerializeField] private int physicsDamage = 2;
    [SerializeField] private int normalDamage= 1;
    [SerializeField] private float DestroyTime = 2.5f;
    private int damage;
    private Rigidbody2D rb;
    private AudioController audioController;
    SpriteRenderer spriteRenderer;
    public Sprite[] els;
    private GameObject enemy,player;
    public float force = 5;
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
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            spriteRenderer.sprite = els[0];
        }
        else if (type == bulletType.Normal)
        {
            rb.gravityScale = 0f;
            spriteRenderer.sprite = els[1];
            AutoShoot();
        }
    }
    private void AutoShoot()
    {
        enemy = GameObject.FindGameObjectWithTag("enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        if (enemy != null && !player)
        {
            float distance=Vector2.Distance(enemy.transform.position, player.transform.position);
            Debug.Log(distance);
            if(distance <10)
            {
                Vector3 direction = enemy.transform.position - transform.position;
                rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
                float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rot + 90);
            }    
           
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
    public void SetBulletType(bulletType newType)
    {
        type = newType;
        initialBulletStats(); // Initialize stats based on the new type
    }
    public void ToggleBulletType()
    {
        // Toggle between Normal and Physics bullet types
        type = (type == bulletType.Normal) ? bulletType.Physics : bulletType.Normal;
        initialBulletStats(); // Reinitialize stats based on the new type
        setRBSStats();
        Debug.Log("Bullet type switched to: " + type);
    }
}
