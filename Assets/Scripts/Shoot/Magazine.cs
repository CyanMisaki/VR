using System;
using UnityEngine;

namespace Shoot
{
    public struct WeaponType
    {
        public enum magazType
        {
            pistol,
            rifle
        };
    }
    public class Magazine: MonoBehaviour
    {
        public WeaponType.magazType typeOfMagazine = WeaponType.magazType.pistol;
        public int ammo = 30;

        [HideInInspector] public bool isOpen = true;
        [HideInInspector] public Rigidbody myRig;
        [HideInInspector] public Collider myCol;
        [HideInInspector] public GameObject Bullet;


        private void Start()
        {
            myRig = GetComponent<Rigidbody>();
            myCol = GetComponent<Collider>();

            if (transform.GetChild(0)) Bullet = transform.GetChild(0).gameObject;

        }
    }
}