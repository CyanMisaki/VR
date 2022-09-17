using System;
using HTC.UnityPlugin.Pointer3D;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HW4
{
    public class MagniteRaycaster : MonoBehaviour
    {
        public enum TypeOfMagnite
        {
            Blue,
            Red,
        }

        [SerializeField] private TypeOfMagnite ColorOfMagnite;

        [Tooltip("Ссылка на Spell персонажа")] [SerializeField]
        private CharMagnetic refToChar;

        private RaycastResult curObj;
        private Pointer3DRaycaster raycaster;

        private void OnValidate()
        {
            raycaster = GetComponent<Pointer3DRaycaster>();
        }

        private void LateUpdate()
        {
            Raycasting();
        }

        private void Raycasting()
        {
            curObj = raycaster.FirstRaycastResult();
        }

        public void StartMagnite()
        {
            if (!curObj.isValid) return;
            
            var rg = curObj.gameObject.GetComponent<Rigidbody>();
                
            switch (ColorOfMagnite)
            {
                case TypeOfMagnite.Blue:
                    if (rg!=null) refToChar.SetBlue(curObj.gameObject.transform);
                    else refToChar.SetBlue(curObj.worldPosition);
                    break;
                case TypeOfMagnite.Red:
                    if(rg!=null) refToChar.SetRed(curObj.gameObject.transform);
                    else refToChar.SetRed(curObj.worldPosition);
                    break;
            }

        }
    }
}