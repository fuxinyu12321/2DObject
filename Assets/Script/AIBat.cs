using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBat : Enemy
{
    public float speed;
    public float radius;
    private Transform playerTransForm;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        playerTransForm=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (playerTransForm != null)
        {
            float distance =(transform.position - playerTransForm.position).sqrMagnitude;
            if (distance < radius)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    playerTransForm.position,speed*Time.deltaTime);
            }
        }
    }
}
