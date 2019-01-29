using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUnit : BaseUnit {

    public new MobileUnitProperties properties;

 	internal override void Start () {

        base.Start();
        properties = new MobileUnitProperties();
        properties.movimentSpeed = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal IEnumerator MoveTo(Vector3[] path)
    {
        for (int i = 0; i < path.Length; i++)
        {
            Vector3 direction = Vector3.zero;
            do
            {
                direction = (path[i] - transform.position);
                MovimentCallBack(direction.normalized);
                yield return null;

            } while (direction.sqrMagnitude > 0.1f);
        }
    }
    public void MovimentCallBack(Vector3 direction)
    {
        
        transform.position += direction.normalized * properties.movimentSpeed * Time.deltaTime;
    }
    public override void ActionCallBack(Vector3 target)
    {
        StartCoroutine(MoveTo(new Vector3[1] { target }));
    }
}
