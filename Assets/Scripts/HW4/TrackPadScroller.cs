using System;
using HTC.UnityPlugin.Vive;
using UnityEngine;
using Valve.VR;

namespace HW4
{
    public class TrackPadScroller : MonoBehaviour
    {
        [SerializeField] private float speed = 10, deadzone = 0.1f;

        private SteamVR_RenderModel vive;
        private CharMagnetic _magnite;

        private void Start()
        {
            _magnite = GetComponent<CharMagnetic>();
        }

        private void Update()
        {
            if (vive == null)
                vive = GetComponentInChildren<SteamVR_RenderModel>();
            var dp = ViveInput.GetPadTouchDelta(HandRole.RightHand).y;

            if (Mathf.Abs(dp) > deadzone)
            {
                _magnite.ChangeSpringPower(dp*speed);
                vive.controllerModeState.bScrollWheelVisible = true;
            }

            if (ViveInput.GetPressUp(HandRole.RightHand, ControllerButton.PadTouch))
                vive.controllerModeState.bScrollWheelVisible = false;
        }
    }
}