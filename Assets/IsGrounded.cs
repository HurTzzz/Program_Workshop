using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour {
    Animator anim;
    private float margin = 0.1f;
    bool isGrounded()
    {
        //雷射判定是否在地面上
        return Physics.Raycast(transform.position, -Vector3.up, margin);
    }
    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	void Update ()
    {
        anim.SetBool("isGrounded", isGrounded());
    }
}
