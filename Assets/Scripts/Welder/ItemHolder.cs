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
                    TakeWeldingHandle(useable);
                    break;
                case UseableTypes.Box:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TakeWeldingHandle(Useable useable)
        {
            useable.transform.parent = _weldingHandlePosition;
            useable.transform.localPosition = Vector3.zero;
            useable.transform.localRotation = Quaternion.identity;
            useable.transform.Rotate(new Vector3(-90f, 0, 0));
            
            useable.GetComponent<Collider>().enabled = false;
        }
    }
}