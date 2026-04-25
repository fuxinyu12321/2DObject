using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float climbSpeed;
    private Rigidbody2D Player;
    private Animator ani;
    private BoxCollider2D feet;
    private bool Ground;
    private bool JumpTimes;
    private int DoubleJump=0;
    public static bool isGameAlive = true;
    private bool isLadder;
    private bool isClimbing;
    private bool isJumping;
    private bool isFalling;
    private bool isDoubleJumping;
    private float playerGravity;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
        playerGravity=Player.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameAlive)
        {
            IsGround();      // 1. 先检测地面
            CheckLadder();   // 2. 检测梯子
            Flip();
            Run();
            Climb();         // 3. 执行攀爬（会修改 Animator 的 Climb 状态）
            Jump();
            HaveJumpTimes();

            CheckStatus();   // 4. 在这里更新状态
            JumpToFall();
            MoveToFall();    // 5. 现在 isClimbing 是最新的了
        }       
    }
    //地面检测
    void IsGround()
    {
        Ground = feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        
    }
    void CheckLadder()
    {
        isLadder= feet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
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
    void Climb()
    {
        if(isLadder)
        {
            float moveY = Input.GetAxis("Vertical");
            if(moveY > 0.1f||moveY<-0.1f)
            {
                ani.SetBool("Climb", true);
                ani.SetBool("Fall", false);
                Player.gravityScale = 0.0f;
                Player.velocity=new Vector2(Player.velocity.x, moveY* climbSpeed);

            }
            else
            {
                if (isJumping || isFalling || isDoubleJumping)
                {
                    ani.SetBool("Climb",false);
                }
                else
                {
                    ani.SetBool("Climb", false);
                    Player.velocity =new Vector2(Player.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            ani.SetBool("Climb",false );
            Player.gravityScale =playerGravity;
        }

    }
    //转换跳跃下落状态
    void MoveToFall() 
    {
        // 只在非攀爬状态下更新Fall状态
        if (!isClimbing)
        {
            if ((!Ground) && Player.velocity.y < -0.1f && !ani.GetBool("Jump") && !ani.GetBool("DoubleJump"))
            {
                ani.SetBool("Fall", true);
            }
            else if (Ground)
            {
                ani.SetBool("Fall", false);
            }
        }
    }
    void JumpToFall()
    {
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
        else if (Ground && !ani.GetBool("Climb"))
        {
            ani.SetBool("Fall", false);
            if (!ani.GetBool("run"))
            {
                ani.SetBool("Idle", true);
            }
        }
    }
    void CheckStatus()
    {
        isJumping = ani.GetBool("Jump");
        isFalling = ani.GetBool("Fall");
        isDoubleJumping = ani.GetBool("DoubleJump");
        isClimbing = ani.GetBool("Climb");
    }
}
