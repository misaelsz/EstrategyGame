using UnityEngine;
using Assets.Scripts.AndroidProject.Tools;
using Assets.Scripts.AndroidProject.Model;
using System.Collections.Generic;

public class MovimentsCharacter : BaseUnit
{
    public float speed = 1f, maxDist = 2;
    private Vector3 newPos, oldPos;
    public Camera camera;
    private bool directionChosen;
    private static List<BaseUnit> _unitInScene;
    private BaseUnit[] _selectedUnits;
    private List<BaseUnit> _deseletedUnits;

    void Awake()
    {
        _unitInScene = new List<BaseUnit>();
        _selectedUnits = new BaseUnit[0];
        _deseletedUnits = new List<BaseUnit>();
    }

    internal override void Start()
    {
        base.Start();
    }



    void Update()
    {
        if (Input.touchCount == 2)
        {
            foreach (BaseUnit unit in _unitInScene)
            {
                unit.IsSelected = false;
            }
           GetUnitsUnderRectangle(Utils.DrawningRect());
        }
        foreach (var unit in _unitInScene)
        {
            if (unit.IsSelected)
            {
                if (Input.touchCount > 0)
                {
                    // Track a single touch as a direction control.
                    if (Input.touchCount == 1)
                    {
                        Touch touch = Input.GetTouch(0);
                        Vector2 screenPos = camera.WorldToScreenPoint(unit.transform.position);
                        print("touch: "+ touch.position);
                        print("unit: " + screenPos);

                        if (touch.position.x < 350f && touch.position.y < 350f)
                        {
                            if (unit.positionOrdened != unit.transform.position)
                            {
                                LookAtFinger(unit.positionOrdened, unit.transform);
                                Move(unit.positionOrdened, unit.transform);
                            }
                        }
                        else
                        {
                            // Handle finger movements based on touch phase.
                            switch (touch.phase)
                            {
                                // Record initial touch position.
                                case TouchPhase.Began:
                                    newPos = Utils.ConvertToVector3(touch.position, unit.transform);
                                    directionChosen = false;
                                    break;

                                // Report that a direction has been chosen when the finger is lifted.
                                case TouchPhase.Ended:
                                    unit.positionOrdened = newPos;
                                    unit.IsOrdened = true;
                                    directionChosen = true;
                                    break;
                            }
                        }


                    }

                }
                if (directionChosen && unit.IsOrdened)
                {
                    LookAtFinger(unit.positionOrdened, unit.transform);
                    Move(unit.positionOrdened, unit.transform);
                }
            }
            else
            {

                if (unit.positionOrdened != unit.transform.position && unit.IsOrdened)
                {
                    LookAtFinger(unit.positionOrdened, unit.transform);
                    Move(unit.positionOrdened, unit.transform);
                }
                else
                {
                    directionChosen = false;
                    unit.IsOrdened = false;
                } 
            }
        }
    }
    public void Move(Vector3 finger, Transform characher)
    {
        if (Vector3.Distance(finger, characher.position) > maxDist)
            characher.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void LookAtFinger(Vector3 finger, Transform myTransform)
    {
        Vector3 directionToLook = finger - myTransform.position;
        myTransform.rotation = Quaternion.LookRotation(directionToLook);
    }
    public static void AddBaseUnitToList(BaseUnit unit)
    {
        _unitInScene.Add(unit);
    }
    private void GetUnitsUnderRectangle(Rect selectionRectangle)
    {
        
        foreach (BaseUnit unit in _unitInScene)
        {
            Vector2 screenPosition = camera.WorldToScreenPoint(unit.transform.position);
            float halfScreen = Screen.height/2;
            if (screenPosition.y > halfScreen)
            {
                float rest = screenPosition.y - halfScreen;
                screenPosition.y = halfScreen - rest;
            }
            else
            {
                float rest = halfScreen - screenPosition.y;
                screenPosition.y = halfScreen + rest;
            }
           // screenPosition = new Vector2(screenPosition.x, -screenPosition.y);
            if (selectionRectangle.Contains(screenPosition))
            {
                unit.IsSelected = true;
                unit.positionOrdened = unit.transform.position;
            }
        }
    }
    public void CleanList<T>(List<T> list)
    {
        foreach (var item in list)
        {
            list.Remove(item);
        }
    }
}