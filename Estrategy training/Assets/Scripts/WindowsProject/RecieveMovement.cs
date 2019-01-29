using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveMovement : MonoBehaviour {


    Vector3 newPosition;
    public float speed;
    public float walkRange;

    public GameObject graphics;

	void Start () {
        newPosition = this.transform.position;
	}
	

	void Update () {
        if (Vector3.Distance(newPosition, this.transform.position) > walkRange)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, newPosition, speed * Time.deltaTime);
            Quaternion transRot = Quaternion.LookRotation(newPosition - this.transform.position, Vector3.up);
            graphics.transform.rotation = Quaternion.Slerp(graphics.transform.rotation, transRot, 0.2f);

        }
	}
    [PunRPC]
    public void RecievedMove(Vector3 movePos)
    {
        newPosition = movePos;
    }
}
