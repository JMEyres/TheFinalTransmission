using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ComputerTurnsOn : BaseStoryEvent
{
    public GameObject computerLog;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered) Debug.Log("ComputerTurnsON");
    }
}
