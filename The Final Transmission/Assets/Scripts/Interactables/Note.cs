using UnityEngine;

public class Note : MonoBehaviour, Interactable
{
    [SerializeField] GameObject noteUI;
    [SerializeField] CameraController cameraController;

    public void Interact()
    {   
        noteUI.SetActive(true);
        cameraController.lockCamera = true;
    }

    void Update()
    {
        if(noteUI.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            noteUI.SetActive(false);
            cameraController.lockCamera = false;
        }
    }
}
