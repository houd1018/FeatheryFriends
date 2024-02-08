using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerSwingDetector : MonoBehaviour
{
    private InputDevice leftController;
    private InputDevice rightController;
    public float swingThreshold = 1.0f; // Set your velocity threshold here

    void Start()
    {
        InitializeControllers();
    }

    void InitializeControllers()
    {
        List<InputDevice> devices = new List<InputDevice>();

        // Find the left controller
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, devices);
        if (devices.Count > 0)
        {
            leftController = devices[0];
        }

        // Find the right controller
        devices.Clear();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);
        if (devices.Count > 0)
        {
            rightController = devices[0];
        }
    }

    void Update()
    {
        bool isLeftSwinging = CheckControllerVelocity(leftController, "Left Controller Velocity: ");
        bool isRightSwinging = CheckControllerVelocity(rightController, "Right Controller Velocity: ");
        if (isLeftSwinging && isRightSwinging)
        {
            Debug.Log("Both controllers are swinging!");
        }
    }

    private bool CheckControllerVelocity(InputDevice controller, string debugMessage)
    {
        if (!controller.isValid)
        {
            InitializeControllers();
            return false;
        }
        else
        {
            Vector3 velocity;
            if (controller.TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity))
            {
                // Use the velocity as needed
                //Debug.Log(debugMessage + velocity.magnitude);
                return velocity.magnitude > swingThreshold;
            }
            return false;
        }
    }
}
