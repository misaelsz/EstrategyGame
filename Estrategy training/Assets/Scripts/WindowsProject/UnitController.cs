using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

    private Vector2 _initialPosition;
    private Vector2 _finalPosition;
    public Texture2D RectangleTexture;
    private static List<BaseUnit> _unitInScene;
    public Camera MainCamera;
    private BaseUnit[] _selectedUnits;

     void Awake()
    {
        _unitInScene = new List<BaseUnit>();
        _selectedUnits = new BaseUnit[0];
    }

     void Update()
    {
        if (Input.GetButtonUp("Fire2"))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                ResourceRoot source = hit.transform.GetComponent<ResourceRoot>();
                if (source != null)
                {
                    foreach (BaseUnit unit in _selectedUnits)
                    {
                        unit.ActionCallBack(source);     
                    }
                    return;
                }
                
                foreach (BaseUnit unit in _selectedUnits)
                {
                    unit.ActionCallBack(hit.point);
                }
            } 
        }
    }
    private void OnGUI()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _initialPosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            
        }
        if (Input.GetButton("Fire1"))
        {
            _finalPosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            GUI.DrawTexture(new Rect(_initialPosition.x, _initialPosition.y, _finalPosition.x - _initialPosition.x, _finalPosition.y - _initialPosition.y), RectangleTexture);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            foreach (BaseUnit unit in _selectedUnits)
            {
                unit.IsSelected = false;
            }

            float xMin = Mathf.Min(_initialPosition.x, _finalPosition.x);
            float yMin = Mathf.Min(_initialPosition.y, _finalPosition.y);
            float width = Mathf.Abs(_initialPosition.x - _finalPosition.x);
            float height = Mathf.Abs(_initialPosition.y = _finalPosition.y);

            _selectedUnits = GetUnitsUnderRectangle(new Rect(xMin, yMin, width, height));

            foreach (BaseUnit unit in _selectedUnits)
            {
                unit.IsSelected = true;
            }
        }
    }

    private BaseUnit[] GetUnitsUnderRectangle(Rect selectionRectangle)
    {
       
        List<BaseUnit> selectedUnits = new List<BaseUnit>();
        foreach (BaseUnit unit in _unitInScene)
        {
            Vector3 unitPositionInScene = MainCamera.WorldToScreenPoint(unit.transform.position);
            Vector2 convertedUnitPosition = new Vector2(unitPositionInScene.x, Screen.height - unitPositionInScene.y);
            if (selectionRectangle.Contains(convertedUnitPosition))
            {
                selectedUnits.Add(unit);
            }
        }
        return selectedUnits.ToArray();
    }

    public static void AddBaseUnitToList(BaseUnit unit)
    {
        _unitInScene.Add(unit);
    }

    public static IResouceReceiver GetClosestResourceReceiver(ResourceType resource, Vector3 relativeTo)
    {
        float minDistance = Mathf.Infinity;
        StorageBuilding closest = null;

        foreach (BaseUnit unit in _unitInScene)
        {
            if (unit is IResouceReceiver)
            {
                float currentDistance = Vector3.Distance(unit.transform.position, relativeTo);
                if (currentDistance < minDistance && (unit as IResouceReceiver).AcceptResource(resource))
                {
                    closest = unit as StorageBuilding;
                    minDistance = currentDistance;
                }
            }
        }
        return closest as IResouceReceiver;
    }
}
