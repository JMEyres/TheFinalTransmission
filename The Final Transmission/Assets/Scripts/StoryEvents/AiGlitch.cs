using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class AiGlitch : BaseStoryEvent
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject computerSmoke;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject crewLog;
    [SerializeField] private AudioSource glitchAudioSource, textAudioSource;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private CameraController cameraController;
    [TextArea] public List<string> glitchText, choice1Text, choice2Text;
    [TextArea] public List<AudioClip> glitchAudioClips, choice1Clips, choice2Clips;
    private List<string> currentText;
    private List<AudioClip> currentClips;
    public float typeSpeed = 0.05f;
    private float timer = 0f;
    private int charIndex = 0;
    private int currentLineIndex = 0;
    private bool isTyping = true;
    private bool lineCompleted, audioPlayed, glitchAudioPlayed, endEvent, endAfterTyping, choiceMade = false;

    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            if(Input.GetKeyDown(KeyCode.Return) || endEvent)
            {
                textObject.SetActive(false);
                computerSmoke.SetActive(false);
                isTyping = false;
                glitchAudioSource.Stop();
                textAudioSource.Stop();
                audioPlayed = true;
                choiceMade = true;
                StoryManager.Instance.ResumeTimeline();
                StoryManager.Instance.TriggerEvent("ConflictingInfo");
                triggered = false;
                return;
            }

            if (!glitchAudioPlayed) 
            {
                computerUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                cameraController.lockCamera = false;
                computerSmoke.SetActive(true);
                glitchAudioSource.Play();
                currentText = glitchText;
                currentClips = glitchAudioClips;
                glitchAudioPlayed = true;
            }

            else if(glitchAudioPlayed && !glitchAudioSource.isPlaying)
            {
                if (isTyping)
                {
                    textObject.SetActive(true);
                    if(currentLineIndex == 0 && currentClips.Count != 0)
                    {
                        textAudioSource.resource=currentClips[currentLineIndex];
                    }

                    if (charIndex < currentText[currentLineIndex].Length)
                    {
                        if(!audioPlayed && currentClips.Count != 0) 
                        {
                            textAudioSource.Play();
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
                        textUI.text = "1. What exactly was I not supposed to see? \n2. I didn’t mean to pry. Let’s just move on. ";
                    }
                    if(Input.GetKeyDown(KeyCode.Alpha1)) // Question AI
                    { 
                        SetAiText(choice1Text, choice1Clips); 
                        StoryManager.Instance.AiRep(5);
                        StoryManager.Instance.savedChoice = "Question";
                        endAfterTyping = true;
                    } 
                    if(Input.GetKeyDown(KeyCode.Alpha2)) // Ignore AI
                    { 
                        StoryManager.Instance.AiRep(-5);
                        StoryManager.Instance.savedChoice = "Ignore";
                        crewLog.SetActive(true); // need to make it so when player closes log it resumes timeline
                        endEvent = true;
                    } 

                    // need to log choice made in story manager as it impacts next story event - should be able to really botch this as its the only choice that specifically needs to be remembered
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
            textAudioSource.resource = currentClips[currentLineIndex];
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
