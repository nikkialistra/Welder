﻿using RoomObjects;
using RoomObjects.Interactables;
using UnityEngine;
using UnityEngine.UI;
using Welder;

namespace UI
{
    [RequireComponent(typeof(RoomRefiner))]
    [RequireComponent(typeof(ActionOutcome))]
    public class PreparableShower : MonoBehaviour
    {
        [SerializeField] private WelderMotionAnimator _welderMotionAnimator;

        [SerializeField] private RectTransform _preparableChoices;
        
        [SerializeField] private Button _startWorking;
        [SerializeField] private Button _removeBoxes;
        [SerializeField] private Button _wipeFloor;

        private RoomRefiner _roomCleaner;
        private Preparable _preparable;

        private ActionOutcome _actionOutcome;
        
        private bool _showing;

        private void Awake()
        {
            _roomCleaner = GetComponent<RoomRefiner>();
            _actionOutcome = GetComponent<ActionOutcome>();
        }

        private void Update()
        {
            if (!_showing)
            {
                return;
            }

            CheckKeyPresses();
        }

        public void Show(Preparable preparable)
        {
            _preparable = preparable;
            
            _preparableChoices.gameObject.SetActive(true);
            
            if (_showing)
            {
                return;
            }

            _showing = true;
        }

        private void CheckKeyPresses()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RemoveBoxes();
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                WipeFloor();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartWorking();
            }
        }

        private void RemoveBoxes()
        {
            _removeBoxes.interactable = false;
            
            _actionOutcome.ShowCorrect();

            _roomCleaner.RemoveBoxes();
        }

        private void WipeFloor()
        {
            _wipeFloor.interactable = false;
            
            _actionOutcome.ShowCorrect();

            _roomCleaner.WipeFloor();
        }

        private void StartWorking()
        {
            _startWorking.interactable = false;

            var boxesNotRemoved = _removeBoxes.IsInteractable();
            var floorNotWiped = _wipeFloor.IsInteractable();

            if (boxesNotRemoved || floorNotWiped)
            {
                _actionOutcome.ShowDanger();
            }
            else
            {
                _actionOutcome.ShowCorrect();
            }
            
            _preparable.gameObject.SetActive(false);
            
            _welderMotionAnimator.StartWorking(boxesNotRemoved, floorNotWiped);
        }
    }
}