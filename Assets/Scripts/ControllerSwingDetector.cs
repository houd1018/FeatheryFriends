using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System; // Add this for UI elements

public class ControllerSwingDetector : MonoBehaviour
{
    public GameManager gameManager;
    private InputDevice leftController;
    private InputDevice rightController;
    private flyController flyController;
    public float swingThreshold = 2.5f; // Set your velocity threshold here
    public float speed = 8f;
    public float increasedSpeedAmount = 25f; // New variable for increased speed

    // ---------record for taking off
    private bool hasflyied_1 = false;
    private bool hasflyied_2 = false;

    [Header("ProgressBar")]
    // Timer and Progress Bar
    public float swingDuration = 0.5f;
    public Image progressBar; // Reference to the UI Image for the progress bar
    private float swingTimer = 0f;
    // private float allowedInterruptionDuration = 2f;
    // private float interruptionTimer = 0f; // Timer to track the interruption duration

    [Header("Boost")]
    public GameObject speedEffect; // Reference to the VFX object
    public float gracePeriodDuration = 1.0f; // Duration of the grace period in seconds
    private float graceTimer = 0f; // Timer for the grace period
    private Boost boost;


    void Start()
    {
        InitializeControllers();
        flyController = GetComponent<flyController>();
        boost = speedEffect.GetComponent<Boost>();

        if (progressBar != null)
            progressBar.fillAmount = 0; // Initialize progress bar to empty
    }

    void Update()
    {
        bool isLeftSwinging = CheckControllerVelocity(leftController, "Left Controller Velocity: ");
        bool isRightSwinging = CheckControllerVelocity(rightController, "Right Controller Velocity: ");
        // Debug.Log(isLeftSwinging);
        // Debug.Log(isRightSwinging);

        // check if it is taking off
        if (flyController.speed == 0 && !gameManager.isFishing)
        {
            UpdateSwingProgress_TakeOff(isLeftSwinging, isRightSwinging);
        }

        if (flyController.speed != 0)
        {
            // boosting
            BoostSpeedAndVFX(isLeftSwinging, isRightSwinging);
        }
    }

    void BoostSpeedAndVFX(bool isLeftSwinging, bool isRightSwinging)
    {
        if (isLeftSwinging && isRightSwinging || graceTimer > 0)
        {
            // If we just started swinging both controllers, reset the grace timer
            if (isLeftSwinging && isRightSwinging)
            {
                graceTimer = gracePeriodDuration;
            }
            else
            {
                // Decrease grace timer if only one or neither controller is swinging
                graceTimer -= Time.deltaTime;
            }


            flyController.speed = increasedSpeedAmount; // Increase to a faster speed

            boost.startBoost(); // Enable VFX when speeding up

        }
        else
        {
            flyController.speed = speed; // Revert to original speed

            boost.endBoost(); // Disable VFX when not speeding up

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
                Debug.Log("Both controllers have been swinging for 0.5 seconds!");
                flyController.speed = speed;
                // Optionally, reset the timer if you want the action to be repeatable
                swingTimer = 0;
                progressBar.fillAmount = 0;


                
                // ---------taking off control
                if (!hasflyied_1)
                {
                    gameManager.mom_currentState = GameManager.MomEagleState.FlyRoute_1;
                    hasflyied_1 = true;
                }

                if (!hasflyied_2 && gameManager.hasfished)
                {
                    gameManager.mom_currentState = GameManager.MomEagleState.FlyRoute_2;
                    hasflyied_2 = true;
                }
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
