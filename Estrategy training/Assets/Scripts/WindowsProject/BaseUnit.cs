using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Projector projector;
    private bool _isSelected;
    public UnitProperties properties;

    public bool IsSelected
    {
        get { return _isSelected;  }
        set
        {
            if (value)
            {
                projector.enabled = true;
            }
            else
            {
                projector.enabled = false;
            }

            _isSelected = value;
        }
    }

     internal virtual void Start()
    {
        properties.Initialize();
        UnitController.AddBaseUnitToList(this);
    }

    public virtual void ActionCallBack(Vector3 target){}
    public virtual void ActionCallBack(ResourceRoot target) { }

}
