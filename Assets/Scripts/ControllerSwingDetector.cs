using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI; // Add this for UI elements

public class ControllerSwingDetector : MonoBehaviour
{
    private InputDevice leftController;
    private InputDevice rightController;
    private flyController flyController;
    public float swingThreshold = 3.0f; // Set your velocity threshold here
    public float speed = 10f;

    [Header("ProgressBar")]
    // Timer and Progress Bar
    public float swingDuration = 1.5f;
    public Image progressBar; // Reference to the UI Image for the progress bar
    private float swingTimer = 0f;
    private float allowedInterruptionDuration = 2f;
    private float interruptionTimer = 0f; // Timer to track the interruption duration


    void Start()
    {
        InitializeControllers();
        flyController = GetComponent<flyController>();

        if (progressBar != null)
            progressBar.fillAmount = 0; // Initialize progress bar to empty
    }

    void Update()
    {
        bool isLeftSwinging = CheckControllerVelocity(leftController, "Left Controller Velocity: ");
        bool isRightSwinging = CheckControllerVelocity(rightController, "Right Controller Velocity: ");
        Debug.Log(isLeftSwinging);
        Debug.Log(isRightSwinging);

        // check if it is taking off
        if (flyController.speed == 0)
        {
            UpdateSwingProgress_TakeOff(isLeftSwinging, isRightSwinging);
        }
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

    private void UpdateSwingProgress_TakeOff(bool isLeftSwinging, bool isRightSwinging)
    {
        if (isLeftSwinging && isRightSwinging)
        {
            // Increment the swing timer
            swingTimer += Time.deltaTime;

            // Update progress bar
            if (progressBar != null)
                progressBar.fillAmount = swingTimer / swingDuration;

            // Check if the swing duration has been met
            if (swingTimer >= swingDuration)
            {
                Debug.Log("Both controllers have been swinging for 5 seconds!");
                flyController.speed = speed;
                // Optionally, reset the timer if you want the action to be repeatable
                swingTimer = 0;
                progressBar.fillAmount = 0;
            }
        }
        //else
        //{
        //    // Start or increment the interruption timer
        //    interruptionTimer += Time.deltaTime;

        //    if (interruptionTimer > allowedInterruptionDuration)
        //    {
        //        // Only reset if the interruption lasts longer than the allowed duration
        //        swingTimer = 0;
        //        if (progressBar != null)
        //            progressBar.fillAmount = 0;
        //        interruptionTimer = 0f; // Reset interruption timer
        //    }
        //    // Note: We do not immediately reset the swing timer or progress bar
        //}
    }
}
