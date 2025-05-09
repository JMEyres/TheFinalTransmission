using System;
using UnityEngine;

public class Computer : MonoBehaviour, Interactable
{
    [SerializeField] GameObject computerScreen;
    public void Interact()
    {
        computerScreen.SetActive(true);
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
        }
    }
}
