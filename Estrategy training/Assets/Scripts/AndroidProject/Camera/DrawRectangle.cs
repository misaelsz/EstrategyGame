using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRectangle : MonoBehaviour
{
    private Vector2 touchOne;
    private Vector2 touchTwo;
   

    private void OnGUI()
    {
        
        if (Input.touchCount == 2)
        {
            touchOne = Input.GetTouch(0).position;
            touchTwo = Input.GetTouch(1).position;
            float width = touchTwo.x - touchOne.x;
            float higth = touchOne.y - touchTwo.y;
            GUI.Box(new Rect(touchOne.x, Screen.height -touchOne.y, width, higth), "");
        }
    }
}
