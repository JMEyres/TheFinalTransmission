using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject computerUI;
    private Interactable currentInteractable;
    public bool computerEnabled = false;

    void Awake()
    {
        eventSystem = EventSystem.current;
    }
    // Update is called once per frame
    void Update()
    {
        computerEnabled = computerUI.activeInHierarchy;
        if(!computerEnabled)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactableLayer))
                {
                    Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                    if(hitInfo.collider.TryGetComponent<Interactable>(out Interactable hitInteractable))
                    {
                        currentInteractable = hitInteractable;
                        currentInteractable.Interact();
                    }
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                currentInteractable = null;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                PointerEventData pointerData = new PointerEventData(eventSystem);
                pointerData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                raycaster.Raycast(pointerData, results);

                foreach (RaycastResult result in results)
                {
                     if (result.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
                    {
                        currentInteractable = interactable;
                        currentInteractable.Interact();
                        break; // Stop at first valid result
                    }
                }
            }
        }
    }
}
