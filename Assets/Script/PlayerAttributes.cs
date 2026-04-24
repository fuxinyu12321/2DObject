using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public int maxHealth;
    public int minHealth;
    public int health;
    public int Chinese;
    public int math;
    public int English;
    public int Physics;
    public int Chemistry;
    public int Politics;
    public int History;
    public int Biology;
    public int Geography;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void healthDown(int dam)
    {
        health -= dam;
    }
}
