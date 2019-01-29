using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Gold,
    Wood,
    Food
}

public class ResourceRoot : MonoBehaviour {

    public ResourceType resource;
    [SerializeField]
    public int resourcesLeft;
    public delegate void EmptySourceEventHandler();
    public event EmptySourceEventHandler OnEmptySource;

    public int GatherFromHere(int desiredAmount)
    {
        if (desiredAmount <= resourcesLeft)
        {
            resourcesLeft -= desiredAmount;
            return desiredAmount;
            
        }
        else
        {
            resourcesLeft = 0;
            if (OnEmptySource != null)
            {
                OnEmptySource();
            }
            return resourcesLeft;
            
        }
    }

    public bool IsEmpty
    {
        get { return resourcesLeft <= 0; }
    }
}
