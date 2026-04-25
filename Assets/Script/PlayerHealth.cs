using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int healthTemp;
    public static int health;
    public int blinks;
    public float time;
    public float PlayerHitBoxCdTime;
    public float deathTime;
    private Renderer myRender;
    private Animator ani;
    private ScreenFlash sf;
    private Rigidbody2D rb2D;
    private PolygonCollider2D polygonCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        health = healthTemp;
        HealthBar.HealthMax = health;
        HealthBar.HealthCurrent = health;
        myRender = GetComponent<Renderer>();
        ani= GetComponent<Animator>();
        sf=GetComponent<ScreenFlash>();
        rb2D=GetComponent<Rigidbody2D>();
        polygonCollider2D=GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamegePlayer(int damage)
    {
        if (!PlayerController.isGameAlive)
        {
            polygonCollider2D.enabled = false;
        }
        else
        {
            sf.FlashScreen();
        }
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        HealthBar.HealthCurrent=health;
        if(health <= 0)
        {
            rb2D.velocity=new Vector2 (0,0);
            ani.SetTrigger("Die");
            PlayerController.isGameAlive = false;
            //Destroy(gameObject,1.3f);
            StartCoroutine(Death());
        }

         BlinkPlayer(blinks, time);
         polygonCollider2D.enabled = false;
         StartCoroutine(showPlayerHitBox());

        
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        gameObject.SetActive(false);
    }
    IEnumerator showPlayerHitBox()
    {
        yield return new WaitForSeconds(PlayerHitBoxCdTime);
        polygonCollider2D.enabled = true;
    }
    void BlinkPlayer(int numBlink,float seconds)
    {
        if (!PlayerController.isGameAlive)
        {
            return;
        }
        StartCoroutine(DoBlinks(numBlink,seconds));
    }
    IEnumerator DoBlinks(int numBlink,float seconds)
    {
        for(int i = 0; i < numBlink*2; i++)
        {
            myRender.enabled=!myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled= true;
    }
}
