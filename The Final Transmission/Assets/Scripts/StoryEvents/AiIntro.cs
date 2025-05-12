using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class AiIntro : BaseStoryEvent
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private GameObject textObject;
    [SerializeField] private AudioSource audioSource;
    private List<string> currentText;
    [TextArea] public List<string> introText, choice1Text, choice2Text, choice3Text, probeChoice1Text, probeChoice2Text;
    private List<AudioClip> currentClips;
    public List<AudioClip> introClips, choice1Clips, choice2Clips, choice3Clips, probeChoice1Clips, probeChoice2Clips;
    public float typeSpeed = 0.05f;
    private float timer = 0f;
    private int charIndex = 0;
    private int currentLineIndex = 0;
    private bool isTyping = true;
    private bool lineCompleted, audioPlayed, choiceMade, probeAI, endEvent, endAfterTyping = false;
    void Awake()
    {
        textUI.text = "";
        currentText = introText;
        currentClips = introClips;
    }
    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            if(Input.GetKeyDown(KeyCode.Return) || endEvent)
            {
                textObject.SetActive(false);
                isTyping = false;
                audioSource.Stop();
                audioPlayed = true;
                choiceMade = true;
                textUI.text = "";
                StoryManager.Instance.ResumeTimeline();
                triggered = false;
                return;
            }

            if (isTyping)
            {
                textObject.SetActive(true);
                if(currentLineIndex == 0 && currentClips.Count != 0)
                {
                    audioSource.resource=currentClips[currentLineIndex];
                }

                if (charIndex < currentText[currentLineIndex].Length)
                {
                    if(!audioPlayed && currentClips.Count != 0) 
                    {
                        audioSource.Play();
                        audioPlayed = true;
                    }
                    timer += Time.deltaTime;

                    if (timer >= typeSpeed)
                    {
                        timer = 0f;
                        textUI.text += currentText[currentLineIndex][charIndex];
                        charIndex++;
                    }
                }
                else if (!lineCompleted)
                {
                    lineCompleted = true;
                    Invoke("NextLine", 2.5f);
                }
                
            }
            else
            {
                if(!choiceMade)
                {
                    textObject.SetActive(true);

                    if(probeAI) textUI.text = "1. Let’s not get lost in speculation. I just want to understand what’s needed now. \n2. You’re avoiding something. What really happened to the crew?";
                    else textUI.text = "1. That’s… a lot to process. But I believe you. \n2. I’ll figure things out on my own. \n3. That’s a convenient version of events. What aren’t you telling me?";
                }
                
                if(probeAI)
                {
                    if(Input.GetKeyDown(KeyCode.Alpha1)) 
                    { 
                        SetAiText(probeChoice1Text, probeChoice1Clips); 
                        endAfterTyping = true; 
                    }
                    if(Input.GetKeyDown(KeyCode.Alpha2)) 
                    { 
                        SetAiText(probeChoice2Text, probeChoice2Clips);
                        StoryManager.Instance.AiRep(-10);
                        endAfterTyping = true;
                    } 
                }
                else
                {
                    if(Input.GetKeyDown(KeyCode.Alpha1)) // Take AI Word
                    { 
                        SetAiText(choice1Text, choice1Clips); 
                        StoryManager.Instance.AiRep(5);
                        endAfterTyping = true;
                    } 
                    if(Input.GetKeyDown(KeyCode.Alpha2)) // Ignore AI
                    { 
                        SetAiText(choice2Text, choice2Clips); 
                        StoryManager.Instance.AiRep(-5);
                        endAfterTyping = true; 
                    } 
                    if(Input.GetKeyDown(KeyCode.Alpha3)) // Probe AI
                    { 
                        SetAiText(choice3Text, choice3Clips); 
                        probeAI = true; 
                        StoryManager.Instance.AiRep(-10);
                        choiceMade = false;
                    } 
                }
            }
        }
    }

    public void NextLine()
    {
        if (currentLineIndex < currentText.Count - 1)
        {
            currentLineIndex++;
            charIndex = 0;
            textUI.text = "";
            lineCompleted = false;
            audioSource.resource = currentClips[currentLineIndex];
            audioPlayed = false;
        }
        else
        {
            textObject.SetActive(false);
            isTyping = false;
            currentLineIndex = 0;
            if(endAfterTyping) endEvent = true;
        }
    }

    private void SetAiText(List<string> text, List<AudioClip> clips) // Set text & audio clips then just reset everything used for text writing
    {
        currentText = text;
        currentClips = clips;
        isTyping = true;
        audioPlayed = false;
        lineCompleted = false;
        textUI.text = "";
        charIndex = 0;
        currentLineIndex = 0;
        choiceMade = true;
    }
}
