using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlAndroid : MonoBehaviour
{
    public float moveSpeed = 8f;
    public Joystick joystick;

    void Update()
    {
        Vector3 moveVector = (Vector3.forward * joystick.Horizontal + Vector3.right * -(joystick.Vertical));

        if (moveVector != Vector3.zero)
        {
          //  transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
