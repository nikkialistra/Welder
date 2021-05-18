using Services;
using UnityEngine;

namespace Welder
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Equipment))]
    public class WelderInteractor : MonoBehaviour
    {
        [SerializeField] private ActionHandler _actionHandler;

        [Space]
        [SerializeField] private float _dangerDistance;

        private Equipment _equipment;

        private void Awake()
        {
            _equipment = GetComponent<Equipment>();
        }

        private void OnInteract()
        {
            // if (!IsValidInteraction(interactable))
            //     return;
            //
            // switch (interactable.GetInteractableType())
            // {
            //     case InteractableType.Raise:
            //         TryRaise(interactable);
            //         break;
            //     case InteractableType.Weld:
            //         TryWeld();
            //         break;
            //     case InteractableType.Equip:
            //         TryEquip(interactable);
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
        }

        private void TryEquip()
        {
            // var equipable = interactable as Equipable;
            // if (equipable == null)
            //     return;
            //
            // _welderEquipment.Equip(equipable);
            // OnInteractableReset();
        }

        private void TryRaise()
        {
            if (!_equipment.Equiped)
            {
                ShowMessage("У меня не полное снаряжение.");
                return;
            }

            // var raisable = interactable as Raisable;
            // if (raisable == null)
            //     return;
            //
            // _raisingObject = raisable;
            // raisable.Raise(transform, _heightForUse, _timeForUse);
        }

        // private void TryWeld()
        // {
        //     if (!_equipment.Equiped)
        //     {
        //         ShowMessage("У меня не полное снаряжение.");
        //         return;
        //     }
        //
        //     if (CheckForDanger())
        //     {
        //         ShowMessage("Бочки рядом, опасно работать.");
        //         return;
        //     }
        // }
        //
        // private bool CheckForDanger()
        // {
        //     return raisables.Any(raisable => Vector3.Distance(transform.position, raisable.transform.position) < _dangerDistance);
        // }

        private static void ShowMessage(string message) => Debug.Log(message);
    }
}