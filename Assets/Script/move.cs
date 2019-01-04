using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*移動流程 : 如果在地面上 移動方式為 當在地面上時 按下移動鍵為普通速度 如果移動+跑步的話 隨著按下跑步的秒數增加 速度會漸漸增加直到速度為X 
                                                  不管是否在移動 如果按下跳躍 人物會進行跳躍動作 當人物落地時 延遲0.X秒才能進行其他動作
                                                  按下跑步時 speed逐漸增加直到x 若是急轉 speed reset 
                                                  跳躍時 
*/
public class move : MonoBehaviour
{
    public bool useMove;
    public Animator anim;
    public Rigidbody rbody;
    public float jump_limit;
    public float jumpforce;
    public float speed_limit;
    public float speed;
    public float inputH;
    private float margin = 0.1f;
    public float gravity;
    private float fallingspeed;
    private float jumpbal = 20f;
    private float jumptimer;
    private bool run;
    private bool jump;
    public bool turnright;

    public bool IsGrounded()
    {
        //雷射判定是否在地面
        return Physics.Raycast(transform.position, -Vector3.up, margin);
    }

    void Start()
    {
        useMove = false;
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
            anim.SetFloat("inputH", inputH);
            anim.SetFloat("speed", speed);
            anim.SetFloat("gravity", gravity);
            anim.SetFloat("Fallingspeed", fallingspeed);
            anim.SetFloat("jumptimer", jumptimer);
            anim.SetBool("jump", jump);
            anim.SetBool("run", run);
            anim.SetBool("turnright", turnright);
            anim.SetBool("IsGrounded", IsGrounded());
            inputH = Input.GetAxis("Horizontal");
            fallingspeed = rbody.velocity.y;
        if (useMove == true)
        {                
            movement(inputH, speed, gravity);
            turnControll();
            runControll();
            fallControll();
            jumpControll();
        }
    }

    //////////////////////
    //    移動
    //////////////////////
    void movement(float h,float s,float g)
    {
        rbody.velocity = new Vector3(h * s, -g , 0);
    }

    //////////////////////
    //   轉向
    //////////////////////
    void turnControll()
    {
        if (Input.GetKeyDown(KeyCode.A) && turnright == true && speed >= 1)//若按下A & 面向右 & 速度大於等於1
        {
            speed = 1;//重置speed                
            turnright = false;//向右
            gameObject.transform.Rotate(new Vector3(0f, 180f, 0f));//轉180度
        }
        if (Input.GetKeyDown(KeyCode.D) && turnright == false && speed >= 1)//若按下D & 面向左 & 速度大於等於1
        {
            speed = 1;//重置speed                
            turnright = true;//向右
            gameObject.transform.Rotate(new Vector3(0f, 180f, 0f));//轉180度
        }
    }

    //////////////////////
    //    跑步
    //////////////////////
    void runControll()
    {
        if (Input.GetKey(KeyCode.LeftShift) && speed < speed_limit && (inputH == 1 || inputH == -1))//若按住左Shift & speed小於speed上限 & inputH等於1或-1
        {
            run = true;//跑步為是
            speed += Time.deltaTime * 2;//speed隨時間增加       
        }
        else
        {
            if (speed > 1)//若speed大於1               
                speed -= Time.deltaTime;//speed隨時間減少                
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))//若放開左Shift
        {
            run = false;//run為否
        }

    }

    //////////////////////
    //   墜落
    //////////////////////
    void fallControll()
    {
        if (IsGrounded() == false && fallingspeed < 0)//若不在地面 & 墜落速度小於1
        {
            gravity += Time.deltaTime * 9.8f;//gravity隨時間增加9.8倍
            jumptimer = 0;//jumptimer重置
        }
        else//在地面上&墜落速度大於1
        {
            gravity = 0;//gravity重置
            jump = false;//jump為否
        }
        if (IsGrounded())//若在地面
        {
            jump = false;//jump為否
            jumpforce = jump_limit;//jumpforce重置
            jumptimer += Time.deltaTime;//jumptimer開始計數
        }
    }

    //////////////////////
    //   跳躍
    //////////////////////
    void jumpControll()
    {
        if (gravity == 0)
        {
            if (Input.GetKey(KeyCode.Space) && jumpforce <= jump_limit && jumptimer > 0.5f && jumpforce > 0)//若按住Space & jumpforce大於0 & jumptimer大於0.5秒
            {
                jump = true;//jump為是 
                jumpforce -= Time.deltaTime * 5;//jumpforce隨時間減少
                rbody.AddForce(Vector3.up * jumpforce * jumpbal);//給予向上 * jumpforce * jumpbal的外力
            }
        }
    }
}
