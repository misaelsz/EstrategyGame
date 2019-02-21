﻿using UnityEngine;
using System.Collections;

public class MovimentsCharacter : MonoBehaviour
{
    public float speed = 1f, maxDist = 2;
    private Vector3 finger, directionToLook;
    private Transform myTrans, camTrans;
    public Vector2 startPos, direction;
    public bool directionChosen;
    void Start()
    {
        myTrans = this.transform;
        camTrans = Camera.main.transform;
    }

    

    void Update()
    {
        // Track a single touch as a direction control.
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
        }
        if (directionChosen)
        {
            LookAtFinger();
        }
    }
    public void Move(Vector3 finger, Transform characher)
    {

        if (Vector3.Distance(finger, characher.position) > maxDist)
            characher.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void LookAtFinger()
    {
        #region GetTouchPositionToVector3
        // create ray from the camera and passing through the touch position:
        if (Input.touchCount == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            // create a logical plane at this object's position
            // and perpendicular to world Y:
            Plane plane = new Plane(Vector3.up, transform.position);
            float distance = 0; // this will return the distance from the camera
            if (plane.Raycast(ray, out distance))
            { // if plane hit...
                finger = ray.GetPoint(distance); // get the point
                                                 // pos has the position in the plane you've touche
                #endregion
                #region LookAt
                directionToLook = finger - myTrans.position;
                transform.rotation = Quaternion.LookRotation(directionToLook);
                #endregion
            }
        }
        //move towards finger if not too close
        Move(finger, myTrans);
    }
}