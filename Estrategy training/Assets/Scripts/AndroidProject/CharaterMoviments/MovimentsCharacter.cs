using UnityEngine;
using Assets.Scripts.AndroidProject.Tools;

public class MovimentsCharacter : MonoBehaviour
{
    public float speed = 1f, maxDist = 2;
    private Vector3 finger, directionToLook,newPos, oldPos;
    private Transform myTrans, camTrans;
    public bool directionChosen;
    void Start()
    {
        myTrans = this.transform;
        camTrans = Camera.main.transform;
    }



    void Update()
    {
        if (Input.touchCount > 0)
        {
            // Track a single touch as a direction control.
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x < 350f && touch.position.y < 350f)
                {
                    if (oldPos != myTrans.position)
                    {
                        LookAtFinger(oldPos, myTrans);
                        Move(oldPos, myTrans);
                    }
                }
                else
                {
                    // Handle finger movements based on touch phase.
                    switch (touch.phase)
                    {
                        // Record initial touch position.
                        case TouchPhase.Began:
                            newPos = Utils.ConvertToVector3(touch.position, myTrans);
                            directionChosen = false;
                            break;

                        // Report that a direction has been chosen when the finger is lifted.
                        case TouchPhase.Ended:
                            oldPos = newPos;
                            directionChosen = true;
                            break;
                    }
                }
            }
        }
        if (directionChosen)
        {
            LookAtFinger(newPos, myTrans);
            Move(newPos, myTrans);
        }
    }
    public void Move(Vector3 finger, Transform characher)
    {
        if (Vector3.Distance(finger, characher.position) > maxDist)
            characher.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void LookAtFinger(Vector3 finger, Transform myTransform)
    {
        directionToLook = finger - myTransform.position;
        transform.rotation = Quaternion.LookRotation(directionToLook);
    }
}