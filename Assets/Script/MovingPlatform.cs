using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float waitTime;
    private float waitTimeTemp;
    public Transform[] movePos; 
    private int i;
    private Transform PlayerTransform;
    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        waitTimeTemp=waitTime;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, speed*Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {
            if (waitTime < 0.0f)
            {
                if(i == 1)
                {
                    i=0;
                }
                else
                {
                    i = 1;
                }
                waitTime = waitTimeTemp;
            }else
            {
                waitTime-=Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.parent=gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.parent = PlayerTransform;
        }
    }
}
