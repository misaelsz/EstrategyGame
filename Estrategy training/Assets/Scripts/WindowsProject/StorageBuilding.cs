using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBuilding : StaticUnit, IResouceReceiver
{
    [SerializeField]
    private ResourceType[] _acceptedResources;
    public ResourceType[] AcceptedResources
    {
        get { return _acceptedResources; }
    }

    public void ReceiveResource(int amount, ResourceType resource)
    {
        //Envia os recursos para o gameManager
    }

    public bool AcceptResource(ResourceType resource)
    {
        foreach (ResourceType acceptedResource in _acceptedResources)
        {
            if (resource == acceptedResource)
            {
                return true;
            }
        }
        return false;
    }
}
