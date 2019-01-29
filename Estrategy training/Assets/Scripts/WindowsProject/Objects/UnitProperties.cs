using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitProperties {
    public float totalLife;
    private float _currentLife;

    public float CurrentLife {
        get { return _currentLife; }
    }

    public void Initialize()
    {
        _currentLife = totalLife;
    }

}
