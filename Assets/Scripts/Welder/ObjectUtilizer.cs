using System;
using RoomObjects.Interactables;
using UnityEngine;

namespace Welder
{
    [RequireComponent(typeof(Movement))]
    public class ObjectUtilizer : MonoBehaviour
    {
        public bool HoldingWeldingHandle { get; private set; }
        
        [SerializeField] private Transform _weldingHandlePosition;
        
        [Space] [SerializeField] private AudioSource _takeWeldingHandle;
        
        
        private Useable _useable;
        
        private WelderAnimator _welderAnimator;
        private MouseLook _mouseLook;

        private void Awake()
        {
            _welderAnimator = GetComponent<WelderAnimator>();
        }

        public void Use(Useable useable)
        {
            switch (useable.UseableType)
            {
                case UseableTypes.WeldingHandle:
                    TakeWeldingHandle(useable);
                    break;
                case UseableTypes.Barrel:
                    BeginWelding();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TakeWeldingHandle(Useable useable)
        {
            _takeWeldingHandle.Play();
            
            var useableTransform = useable.transform;
            
            useableTransform.parent = _weldingHandlePosition;
            useableTransform.localPosition = Vector3.zero;
            useableTransform.localRotation = Quaternion.identity;
            useableTransform.Rotate(new Vector3(-90f, 0, 0));
            
            useable.GetComponent<Collider>().enabled = false;

            HoldingWeldingHandle = true;
        }

        private void BeginWelding()
        {
            _welderAnimator.SitDown();
        }
    }
}