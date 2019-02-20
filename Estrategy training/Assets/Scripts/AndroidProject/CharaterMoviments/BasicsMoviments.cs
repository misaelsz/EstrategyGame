using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicsMoviments : MonoBehaviour
{
    [SerializeField]
    private Transform characher;

    private Vector2 touch;
    private Vector3 directionToLook, fingerPosition, whereToMove;
    public float speed = 50f, currentDistance, previousDistace;
    bool isMovement = false;

    Rigidbody rb;

    void Start()
    {
        characher = this.transform;
        fingerPosition = new Vector3(0, 0, 0);
        touch = new Vector2(0, 0);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Ended))
        {

            #region GetTouchPositionToVector3
            // create ray from the camera and passing through the touch position:
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            // create a logical plane at this object's position
            // and perpendicular to world Y:
            Plane plane = new Plane(Vector3.up, transform.position);
            float distance = 0; // this will return the distance from the camera
            if (plane.Raycast(ray, out distance))
            { // if plane hit...
                fingerPosition = ray.GetPoint(distance); // get the point
                                                         // pos has the position in the plane you've touche
                #endregion
                #region LookAt
                directionToLook = fingerPosition - characher.position;
                transform.rotation = Quaternion.LookRotation(directionToLook);
                #endregion   
            }
            // ActionCallBack(fingerPosition);


            if (isMovement)
                currentDistance = (fingerPosition - transform.position).magnitude;


            previousDistace = 0f;
            currentDistance = 0f;
            isMovement = true;
            whereToMove = (fingerPosition - transform.position).normalized;
            rb.velocity = new Vector3(whereToMove.x * speed, whereToMove.y * speed, whereToMove.z * speed);



            if (isMovement)
                previousDistace = (fingerPosition - transform.position).magnitude;
            if (transform.position == fingerPosition)
            {
                print("ta igual");
                isMovement = false;
                rb.velocity = Vector3.zero;
            }
            print(string.Format("{0}   {1}", transform.position, fingerPosition));

        }


    }
}