using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerSwingDetector : MonoBehaviour
{
    private InputDevice leftController;
    private InputDevice rightController;
    private flyController flyController;
    private bool hasSwinged = false;
    public float swingThreshold = 3.0f; // Set your velocity threshold here
    public float speed = 5f;

    void Start()
    {
        InitializeControllers();
        flyController = GetComponent<flyController>();
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
            flyController.speed = speed;
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
