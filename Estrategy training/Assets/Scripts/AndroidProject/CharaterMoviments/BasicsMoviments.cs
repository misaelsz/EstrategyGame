using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicsMoviments : MonoBehaviour
{
    [SerializeField]
    private Transform characher;
    private Vector3 fingerPosition;
    private Vector2 touch;
    Vector3 directionToLook;
    // Start is called before the first frame update
    void Start()
    {
        characher = this.transform;
        fingerPosition = new Vector3(0, 0, 0);
        touch = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
                                                         // pos has the position in the plane you've touched

                #endregion

                #region LookAt
                directionToLook = fingerPosition - characher.position;
                transform.rotation = Quaternion.LookRotation(directionToLook);
                #endregion
                print("target: "+ directionToLook.normalized + "  position: "+ transform.position);

                do
                {
                    directionToLook = fingerPosition - transform.position;
                    transform.position += directionToLook.normalized * 0.1f ; 
                } while (directionToLook.sqrMagnitude > 0.1f);
            }
        }
    }
}