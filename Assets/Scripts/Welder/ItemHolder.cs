using System;
using RoomObjects.Interactables;
using UnityEngine;

namespace Welder
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private Transform _weldingHandlePosition;
        
        private Useable _useable;

        public void Take(Useable useable)
        {
            switch (useable.UseableType)
            {
                case UseableTypes.WeldingHandle:
                    useable.transform.parent = _weldingHandlePosition;
                    useable.transform.localPosition = Vector3.zero;
                    useable.transform.localRotation = Quaternion.identity;
                    useable.transform.Rotate(new Vector3(-90f, 0, 0));
                    break;
                case UseableTypes.Box:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}