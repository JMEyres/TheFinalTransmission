using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private CameraController cameraController;
    private Interactable currentInteractable;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactableLayer))
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                if(hitInfo.collider.TryGetComponent<Interactable>(out Interactable hitInteractable))
                {
                    cameraController.lockCamera = true;
                    currentInteractable = hitInteractable;
                    currentInteractable.Interact();
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            currentInteractable = null;
            cameraController.lockCamera = false;

        }
    }
}
