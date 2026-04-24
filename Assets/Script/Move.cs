using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }
    void Run()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(horizontal, vertical);
        if (horizontal != 0)
        {
            ani.SetFloat("Horizontal", horizontal);
            ani.SetFloat("Vertical", 0);
        }
        if (vertical != 0)
        {
            ani.SetFloat("Vertical", vertical);
            ani.SetFloat("Horizontal", 0);
        }
        ani.SetFloat("Speed", dir.magnitude);


        rb.velocity = dir * 0.5f;
    }
}
