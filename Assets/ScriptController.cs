using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptController : MonoBehaviour {
    move move;
    Climb_IK climb;
    

	void Start ()
    {
        move = gameObject.GetComponent<move>();
        climb = gameObject.GetComponent<Climb_IK>();
        move.useMove = true;
    }
	
	void Update ()
    {
		if(climb.useIK == true)
        {
            move.useMove = false;
        }
        else
        {
            move.useMove = true;
            climb.useIK = false;
        }
	}
}
