using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class GetPressDown_ViveInput : MonoBehaviour
{
    public GameObject princess;
    private void Update()
    {

        if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Grip))
        {
            princess.transform.Rotate(new Vector3(0, 45, 0));

        }
        if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Grip))
        {
            princess.transform.Rotate(new Vector3(0, -45, 0));

        }

    }
}
