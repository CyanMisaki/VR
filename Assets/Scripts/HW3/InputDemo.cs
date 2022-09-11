using System;
using HTC.UnityPlugin.Vive;
using UnityEngine;

namespace HW3
{
    public class InputDemo : MonoBehaviour
    {
        private void Update()
        {
            if (ViveInput.GetPressEx(HandRole.RightHand, ControllerButton.Trigger))
            {
                print("GetPressDownEx Trigger");
            }
            if (ViveInput.GetPressEx(HandRole.RightHand, ControllerButton.Grip))
            {
                print("GetPressDownEx Grip");
            }
            if (ViveInput.GetAxisEx(HandRole.LeftHand, ControllerAxis.Trigger)>0.5f)
            {
                print("GetPressDownEx Trigger"+ ViveInput.GetAxisEx(HandRole.RightHand, ControllerAxis.Trigger));
            }
            
            var dpX=ViveInput.GetPadTouchDelta(HandRole.RightHand).x;
            var dpY = ViveInput.GetPadTouchDelta(HandRole.RightHand).y;

            print("dpX: " + dpX + "; dpY: " + dpY);
        }
    }
}