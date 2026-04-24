using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damege;
    private SpriteRenderer sr;
    private Color originalColor;
    public float FlashTime;
    public GameObject bloodEffect;
    public PlayerHealth playerHealth;
    // Start is called before the first frame update
    public void Start()
    {
        playerHealth =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>(); 
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int whatDamage)
    {
        health -= whatDamage;
        FlashColor(FlashTime);
        Instantiate(bloodEffect,transform.position,Quaternion.identity);
    }

    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }
    void ResetColor()
    {
        sr.color = originalColor;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player")&&other.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)  
            {
                playerHealth.DamegePlayer(damege);
            }
            
        }
    }
}
