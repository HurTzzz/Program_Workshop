using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public bool jump;
    public bool run;
    public bool useMovement;
    public bool move;
    public float speed;
    public float speedLimit;
    public float jumpSpeed;
    public float jumpLimit;
    public float inputH;
    public float gravity;
    public float speedBalanced;
    public float jumpBalanced;    
    Rigidbody rbody;
    public float fallingSpeed;
    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }

	void Start ()
    {
        useMovement = true;
        move = false;
        rbody = gameObject.GetComponent<Rigidbody>();
        jumpSpeed = 5;
        speed = 1;
        gravity = 0;
	}
	
	void Update ()
    {
        inputH = Input.GetAxis("Horizontal");
        fallingSpeed = rbody.velocity.y;
	}

    void FixedUpdate()
    {
        if (useMovement)
        {
            moveControll();
            runControll();
            jumpControll();
            gravityControll();
        }       
    }
    void moveControll()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))       
            move = true;        
        else        
            move = false;
        if (move)       
            rbody.velocity = new Vector3(inputH * speed * speedBalanced, -gravity, 0);       
    }

    void runControll()
    {
        if(Input.GetKey(KeyCode.LeftShift)&&speed <= speedLimit&&move == true)
        {
            run = true;
            speed += Time.deltaTime;
        }
        else
        {
            if(speed > 1)
            speed -= Time.deltaTime;
            run = false;
        }
    }

    void jumpControll()
    {
        if(Input.GetKey(KeyCode.Space)&&jumpSpeed > 0)
        {
            jump = true;
            jumpSpeed -= Time.deltaTime * 5;            
        }
        else
        {
            jump = false;
            jumpSpeed = jumpLimit;
        }
        if(jump)
        {
            rbody.AddForce(Vector3.up * jumpSpeed * jumpBalanced);
        }
    }

    void gravityControll()
    {
        if (!isGrounded()&&fallingSpeed < 0)
        {
            gravity += Time.deltaTime * 9.8f;
        }
        else
        {
            gravity = 0;
        }
    }

   
}
