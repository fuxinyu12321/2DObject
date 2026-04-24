using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    private Renderer myRender;
    public int blinks;
    public float time;
    private Animator ani;
    private ScreenFlash sf;
    private Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.HealthMax = health;
        HealthBar.HealthCurrent = health;
        myRender = GetComponent<Renderer>();
        ani= GetComponent<Animator>();
        sf=GetComponent<ScreenFlash>();
        rb2D=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamegePlayer(int damage)
    {
        sf.FlashScreen();
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
            Destroy(gameObject,1.3f);
        }

        BlinkPlayer(blinks, time);
    }
    void BlinkPlayer(int numBlink,float seconds)
    {
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
