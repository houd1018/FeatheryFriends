using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(TeleportationProvider))]
public class TeleportationManager : MonoBehaviour
{

    [SerializeField]
    InputActionReference teleportActivateAction;

    [SerializeField]
    XRRayInteractor teleportRayInteractor;



    bool teleporAcive = false;

    private void OnEnable()
    {
        teleportActivateAction.action.performed += OnTeleportActivate;
        teleportActivateAction.action.canceled += OnTeleportFinished;
    }

    

    RaycastHit hitInfo;
    
    private void OnTeleportActivate(InputAction.CallbackContext obj)
    {
        if(obj.ReadValue<Vector2>().y > 0.6f)
        {

            teleportRayInteractor.enabled = true;
            if(teleportRayInteractor.TryGetCurrent3DRaycastHit(out hitInfo))
            {
                Debug.Log("Found Valid Teleportation Point");
            }
            else
                Debug.Log("InValid Teleportation Point");
        }
    }



    private void OnTeleportFinished(InputAction.CallbackContext obj)
    {
        TeleportRequest teleportRequest = new TeleportRequest()
        {
            destinationPosition = hitInfo.point,
        };

        GetComponent<TeleportationProvider>().QueueTeleportRequest(teleportRequest);
        teleportRayInteractor.enabled = false;

    }



    // Start is called before the first frame update
    void Start()
    {
        teleportRayInteractor.enabled = false;    
    }

}
