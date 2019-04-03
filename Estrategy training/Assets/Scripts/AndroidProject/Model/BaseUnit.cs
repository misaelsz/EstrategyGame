using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AndroidProject.Model
{
    public class BaseUnit:MonoBehaviour
    {
        public Projector projector;
        private bool _isSelected;
        public bool IsOrdened = false;
        public Vector3 positionOrdened;

        public bool IsSelected
        {
            get { return _isSelected; }
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
            MovimentsCharacter.AddBaseUnitToList(this);
        }
    }
}
