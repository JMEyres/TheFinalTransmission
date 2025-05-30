using UnityEngine;

public class InteractableDrawer : MonoBehaviour, Interactable
{
    public float openDistance;
    public float sensitivity = 0.01f;
    public Transform drawer;
    private bool isDragging = false;
    private Vector3 startingPos;
    [SerializeField] private CameraController cameraController;

    void Awake()
    {
        startingPos = drawer.localPosition;
    }

    public void Interact()
    {
        isDragging = true;
    }

    void Update()
    {
        if(isDragging)
        {
            if(Input.GetMouseButton(0))
            {
                cameraController.lockCamera = true;

                float deltaX = Input.GetAxis("Mouse X");
                float deltaY = Input.GetAxis("Mouse Y");

                float move = deltaX * sensitivity; // Need to figure out how to make it so you drag in the forward direction of the item
                Vector3 pos = drawer.localPosition;
                
                pos.z = Mathf.Clamp(pos.z + move, startingPos.z, openDistance);
                drawer.localPosition = pos;
            }
            else
            {
                isDragging = false;
                cameraController.lockCamera = false;
            }
        }
    }
}
