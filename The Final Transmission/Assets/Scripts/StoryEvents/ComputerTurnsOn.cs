using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ComputerTurnsOn : BaseStoryEvent
{
    [SerializeField] private GameObject computerScreenOff;
    [SerializeField] private GameObject computerScreenOn;
    [SerializeField] private GlitchLogTrigger glitchLogTrigger;
    [SerializeField] private MeshRenderer screenMesh;
    public float flickerOnSpeed = 0.1f;
    public float flickerOffSpeed = 0.1f;
    public int maxFlickers = 5;
    private float timer = 0f;
    private int flickerCount = 0;
    private bool flickering = true;
    private bool screenOn, endEvent = false;

    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            if(Input.GetKeyDown(KeyCode.Return) || endEvent)
            {
                screenMesh.material.color = Color.white;
                screenOn = true;
                StoryManager.Instance.ResumeTimeline();
                StoryManager.Instance.TriggerEvent("AiGlitch");
                glitchLogTrigger.triggeredEvent = true;
                triggered = false;
                return;
            }
            computerScreenOff.SetActive(false);
            if (flickering)
            {
                timer += Time.deltaTime;

                if (screenOn && timer >= flickerOnSpeed)
                {
                    screenMesh.material.color = Color.black;
                    screenOn = false;
                    timer = 0f;
                    flickerCount++;
                }
                else if (!screenOn && timer >= flickerOffSpeed)
                {
                    screenMesh.material.color = Color.white;
                    screenOn = true;
                    timer = 0f;
                }

                if (flickerCount >= maxFlickers)
                {
                    flickering = false;
                    screenMesh.material.color = Color.white; // Final state
                }
            }
            else endEvent = true;
        }
    }
}
