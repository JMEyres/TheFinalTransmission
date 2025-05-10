using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class AiIntro : BaseStoryEvent
{
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] GameObject textObject;
    [SerializeField] AudioSource audioSource;
    private List<string> currentText;
    [TextArea] public List<string> introText, choice1Text, choice2Text, choice3Text, probeChoice1Text, probeChoice2Text;
    private List<AudioClip> currentClips;
    public List<AudioClip> introClips, choice1Clips, choice2Clips, choice3Clips, probeChoice1Clips, probeChoice2Clips;
    public float typeSpeed = 0.05f;
    private float timer = 0f;
    private int charIndex = 0;
    private int currentLineIndex = 0;
    private bool isTyping = true;
    private bool lineCompleted, audioPlayed, choiceMade, probeAI, endEvent = false;
    void Start()
    {
        textUI.text = "";
        currentText = introText;
        currentClips = introClips;
        audioSource.playOnAwake = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
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
                if(endEvent) 
                {
                    textObject.SetActive(false);
                    isTyping = false;
                    StoryManager.Instance.ResumeTimeline();
                    triggered = false;
                }
                if(!choiceMade)
                {
                    textObject.SetActive(true);

                    if(probeAI) textUI.text = "Choices 1,2";
                    else textUI.text = "Choices 1,2,3";
                }
                
                if(probeAI)
                {
                    if(Input.GetKeyDown(KeyCode.Alpha1)) { Debug.Log("Choice 1"); SetAiText(probeChoice1Text, probeChoice1Clips); endEvent = true; }
                    if(Input.GetKeyDown(KeyCode.Alpha2)) { Debug.Log("Choice 2"); SetAiText(probeChoice2Text, probeChoice2Clips); endEvent = true; } 
                }
                else
                {
                    if(Input.GetKeyDown(KeyCode.Alpha1)) { Debug.Log("Choice 1"); SetAiText(choice1Text, choice1Clips); endEvent = true; } // Take AI Word
                    if(Input.GetKeyDown(KeyCode.Alpha2)) { Debug.Log("Choice 2"); SetAiText(choice2Text, choice2Clips); endEvent = true; } // Ignore AI
                    if(Input.GetKeyDown(KeyCode.Alpha3)) { Debug.Log("Choice 3"); SetAiText(choice3Text, choice3Clips); probeAI = true; choiceMade = false;} // Probe AI
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
