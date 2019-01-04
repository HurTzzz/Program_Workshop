using UnityEngine;

public class ClimbCheck : MonoBehaviour {

    public move mv;
    private float margin = 0.5f;
    bool Climbcheck()
    {
        //雷射判定是否在地面
        return Physics.Raycast(transform.position, transform.forward, margin);
    }
    void Start ()
    {
    }
	
	
	void Update ()
    {
        mv.anim.SetBool("ClimbCheck",Climbcheck());
	}
}
