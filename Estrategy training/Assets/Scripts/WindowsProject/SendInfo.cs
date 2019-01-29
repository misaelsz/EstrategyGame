using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool RMB = Input.GetMouseButtonDown(1);

        if (RMB)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Ground")
            {
                this.GetComponent<PhotonView>().RPC("RecievedMove", RpcTarget.All, hit.point);
            }
        }
	}
}
