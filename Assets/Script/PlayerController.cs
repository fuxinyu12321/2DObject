using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    private Rigidbody2D Player;
    private Animator ani;
    private BoxCollider2D feet;
    private bool Ground;
    private bool JumpTimes;
    private int DoubleJump=0;
    public static bool isGameAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameAlive)
        {
            Flip();
            Run();
            Jump();
            IsGround();
            HaveJumpTimes();
            JumpToFall();
            MoveToFall();
        }       
    }
    //地面检测
    void IsGround()
    {
        Ground = feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        
    }
    //二段跳
    void HaveJumpTimes()
    {
        if (Ground)
        {
            DoubleJump = 1;
            JumpTimes = false;
            
        }
        JumpTimes = (DoubleJump > 0);
    }
    //模型翻转
    void Flip()
    {
        //bool playerHasxAxisSpeed = Mathf.Abs(Player.velocity.x) > Mathf.Epsilon;
        //if (playerHasxAxisSpeed)
        //{
        //    if(Player.velocity.x > 0.1f)
        //    {
        //        transform.localRotation=Quaternion.Euler(0, 0, 0);
        //    }
        //    if (Player.velocity.x < -0.1f)
        //    {
        //        transform.localRotation = Quaternion.Euler(0, 180, 0);
        //    }
        //}
        if (Mathf.Abs(Player.velocity.x) > 0.1f)
        {
            if (Player.velocity.x > 0)
                transform.localScale = new Vector3(1, 1, 1);   // 面向右
            else if (Player.velocity.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);  // 面向左
        }
    }
    
    
    void Run()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 vec=new (horizontal*runSpeed,Player.velocity.y ); 
        Player.velocity = vec;
        bool playerHasxAxisSpeed = Mathf.Abs(horizontal) > Mathf.Epsilon;
        ani.SetBool("run", playerHasxAxisSpeed);
    }
    void Jump()
    { 
        if (Input.GetButtonDown("Jump") && DoubleJump > 0)
        {
            Vector2 jumpVel = new Vector2(Player.velocity.x, jumpSpeed);
            Player.velocity = jumpVel;
            DoubleJump--;
            if(Ground)
            {
                ani.SetBool("Jump",true);

            }
            else
            {
                ani.SetBool("DoubleJump", true);
                ani.SetBool("Jump", false); 
                ani.SetBool("Fall", false);
            }
        }
        
    }
    //转换跳跃下落状态
    void MoveToFall() 
    {
        // 只有不在地面上，并且向下运动时才是下落
        if (!Ground && Player.velocity.y < -0.1f)
        {
            ani.SetBool("Fall", true);
        }
        else if (Ground)
        {
            // 在地面上时，确保不播放下落动画
            ani.SetBool("Fall", false);
        }

    }
    void JumpToFall()
    {
        ani.SetBool("Idle",false);
        if (ani.GetBool("Jump"))
        {
            if (Player.velocity.y < 0.0f)
            {
                ani.SetBool("Jump", false);
                ani.SetBool("Fall",true );
            }
        }
        else if (ani.GetBool("DoubleJump"))
        {
            if (Player.velocity.y < 0.0f)
            {
                ani.SetBool("DoubleJump", false);
                ani.SetBool("Fall", true);
            }
        }
        else if (Ground)
        {
            ani.SetBool("Fall", false);
            ani.SetBool("Idle", true);
        }
    }
}
