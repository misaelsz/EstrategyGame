using UnityEngine;
using Assets.Scripts.AndroidProject.Tools;
using Assets.Scripts.AndroidProject.Model;
using System.Collections.Generic;

public class MovimentsCharacter : BaseUnit
{
    public float speed = 1f, maxDist = 2;
    private Vector3 finger, directionToLook, newPos, oldPos;
    private Transform myTrans;
    public Camera camera;
    private bool directionChosen;
    private static List<BaseUnit> _unitInScene;
    private BaseUnit[] _selectedUnits;

    void Awake()
    {
        _unitInScene = new List<BaseUnit>();
        _selectedUnits = new BaseUnit[0];
    }

    internal override void Start()
    {
        base.Start();
        myTrans = this.transform;
    }



    void Update()
    {
        if (Input.touchCount > 0)
        {
            // Track a single touch as a direction control.
            if (Input.touchCount == 1 && _selectedUnits.Length > 0)
            {
                Touch touch = Input.GetTouch(0);
                foreach (var unit in _selectedUnits)
                {
                    if (unit.IsSelected)
                    {
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
            }
            if (Input.touchCount == 2)
            {
                foreach (BaseUnit unit in _selectedUnits) { 
                    unit.IsSelected = false;
                }
                _selectedUnits = GetUnitsUnderRectangle(Utils.DrawningRect());
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
    public static void AddBaseUnitToList(BaseUnit unit)
    {
        _unitInScene.Add(unit);
    }
    private BaseUnit[] GetUnitsUnderRectangle(Rect selectionRectangle)
    {
        List<BaseUnit> selectedUnits = new List<BaseUnit>();
        Vector2 unitPositionOnScreen;
        foreach (BaseUnit unit in _unitInScene)
        {
            unitPositionOnScreen = camera.WorldToScreenPoint(unit.transform.position);
            print(selectionRectangle.Contains(unitPositionOnScreen));
            if (selectionRectangle.Contains(unitPositionOnScreen))
            {
                unit.IsSelected = true;
                selectedUnits.Add(unit);
            }
        }
        return selectedUnits.ToArray();
    }
}