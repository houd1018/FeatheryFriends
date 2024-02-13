using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using TMPro;

public class TriggerInputDetector : MonoBehaviour
{


    private InputData _inputData;
    public bool _isPlaying = false;
    private bool _isleft = false;
    private bool _isright = false;

    private void Start()
    {
        _inputData = GetComponent<InputData>();
        Debug.Log("Started inputData: " + _inputData);
    }
    // Update is called once per frame
    void Update()
    {
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue))
        {
            //Debug.Log("leftTriggerValue: " + leftTriggerValue);
            if(leftTriggerValue != 0f)
            {
                _isleft = true;
            }
            else
            {
                _isleft = false;
            }
        }

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue))
        {

            //Debug.Log("rightTriggerValue: " + rightTriggerValue);
            if (rightTriggerValue != 0f)
            {
                _isright = true;
            }
            else
            {
                _isright = false;
            }
        }

        if(_isright && _isleft)
        {
            _isPlaying = true;
        }
        else
        {
            _isPlaying = false;
        }






    }
}