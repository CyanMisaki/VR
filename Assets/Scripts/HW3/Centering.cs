using System;
using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace HW3
{
    public class Centering : MonoBehaviour
    {
        [SerializeField] private Transform pivot;
        [SerializeField] private CapsuleCollider myCol;

        private Vector3 vec;

        private void OnValidate()
        {
            myCol = GetComponent<CapsuleCollider>();
        }

        private void Start()
        {
            FindTeleportPivotAndTarget();
            vec.y = myCol.center.y;
        }

        private void FindTeleportPivotAndTarget()
        {
            foreach (var cam in Camera.allCameras)
            {
                if(!cam.enabled) continue;
                if(cam.stereoTargetEye!=StereoTargetEyeMask.Both) continue;
                pivot = cam.transform;
            }
        }

        private void Update()
        {
            vec.x = pivot.localPosition.x;
            vec.y = pivot.localPosition.y;

            myCol.center = vec;
        }
    }
}