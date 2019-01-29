using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MobileUnit {

    public new VillagerProperties properties;

    internal override void Start()
    {
        base.Start();
        properties = new VillagerProperties();
        properties.resourceCapacity = 15;
        properties.unitsGatheredPerSecond = 2;
    }

    private IEnumerator Gather(ResourceRoot source)
    {
        properties.currentResource = source.resource;
        while(!source.IsEmpty)
        {
            yield return StartCoroutine(MoveTo(new Vector3[1] { source.transform.position }));
            while (!properties.IsFull)
            {
                int unitsThatWillBeGathered = Mathf.Min(properties.unitsGatheredPerSecond, (properties.resourceCapacity - properties.currentResourceAmount));
                print("resourceCapacity: " + properties.resourceCapacity+ " - currentResourceAmount: " + properties.currentResourceAmount + " = unitsThatWillBeGathered: " + unitsThatWillBeGathered);
                properties.currentResourceAmount += source.GatherFromHere(unitsThatWillBeGathered);
                yield return new WaitForSeconds(1);
            }
            IResouceReceiver resouceReceiver = UnitController.GetClosestResourceReceiver(source.resource, transform.position);
            yield return StartCoroutine(MoveTo(new Vector3[1] { (resouceReceiver as StorageBuilding).transform.position}));
            properties.GiveResources(resouceReceiver);
        }
    }
    public override void ActionCallBack(ResourceRoot target)
    {
        StartCoroutine(Gather(target));
    }
}

