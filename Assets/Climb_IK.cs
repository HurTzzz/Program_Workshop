using UnityEngine;
using System.Collections;

public class Climb_IK : MonoBehaviour {
    move move;
    public bool useIK;

    public bool leftHandIK;
    public bool rightHandIK;
    public bool ikControll;
    public Vector3 leftHandPos;
    public Vector3 rightHandPos;

    public float IkTimer;

    private Animator anim;

    void Start ()
    {
        move = gameObject.GetComponent<move>();
        anim = GetComponent<Animator>();
        ikControll = true;
    }		
	void FixedUpdate ()
    {
        RaycastHit LHit;
        RaycastHit RHit;

        if (ikControll)
        {
            if (Physics.Raycast(transform.position + new Vector3(-0.25f, 1.5f, 0f), -transform.up + new Vector3(0f, 0.5f, 0f), out LHit, 1f) && move.turnright == false && move.IsGrounded() == false)
            {
                useIK = true;
                rightHandIK = true;
                leftHandIK = true;
                leftHandPos = LHit.point;
            }
            else
            {
                rightHandIK = false;
                leftHandIK = false;
            }

            if (Physics.Raycast(transform.position + new Vector3(0.25f, 2.0f, 0f), -transform.up + new Vector3(0f, 0.0f, 0.0f), out RHit, 1f) && move.turnright && move.IsGrounded() == false)
            {
                useIK = true;
                rightHandIK = true;
                leftHandIK = true;
                rightHandPos = RHit.point;
                leftHandPos = LHit.point;
            }
            else
            {
                rightHandIK = false;
                leftHandIK = false;
            }
        }

        if(useIK)
        {
            move.gravity = 0;
            move.speed = 0;
            move.rbody.useGravity = false;
            IkTimer += Time.deltaTime;
            if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space)) && IkTimer > 0.5)
            {
                move.speed = 1;
                move.rbody.useGravity = true;
                useIK = false;
                ikControll = false;
                IkTimer = 0;
            }
        }

        if(!useIK)
        {
            if(move.IsGrounded())
            {
                ikControll = true;
            }
        }
    }
    void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(-0.25f, 1.5f, 0f),-transform.up + new Vector3(0f,0.5f,0f), Color.green);
        Debug.DrawRay(transform.position + new Vector3(0.25f, 2.0f, 0f), -transform.up + new Vector3(0f, 0.0f, 0.0f), Color.red);
    }
    void OnAnimatorIK()
    {
        if(useIK)
        {
            if(leftHandIK||rightHandIK)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos);
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandPos);
            }
        }
    }
}
