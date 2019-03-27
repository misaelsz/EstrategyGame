using Assets.Scripts.AndroidProject.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRectangle : MonoBehaviour
{


    private void OnGUI()
    {
        GUI.Box(Utils.DrawningRect(), "");
    }
}
