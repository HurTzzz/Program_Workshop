using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    Vector3 zero;

	void Start () {
		
	}
	void Update ()
    {
        follow();
	}
    public void follow()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position - Vector3.forward * 10, 0.1f);
    }
}
