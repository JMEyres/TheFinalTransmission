using System;
using UnityEngine;

public class Computer : MonoBehaviour, Interactable
{
    [SerializeField] GameObject computerScreen;
    [SerializeField] CameraController cameraController;
    [SerializeField] PlayerInteraction playerInteraction;
    public void Interact()
    {
        computerScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        cameraController.lockCamera = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            computerScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            cameraController.lockCamera = false;
        }
    }
}
