using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockOut : BaseStoryEvent
{
    [SerializeField] GameObject computerScreen, lockOutScreen;
    [SerializeField] AudioSource textAudioSource, systemOverrideSource;

    [SerializeField] List<CodeButtons> buttonValues;
    [SerializeField] GameObject textObject;
    [SerializeField] TextMeshProUGUI textUI;
    [TextArea] public List<string> angryText;
    public List<AudioClip> angryAudioClips;
    private List<string> currentText;
    private List<AudioClip> currentClips;
    public float typeSpeed = 0.05f;
    private float timer = 0f;
    private int charIndex = 0;
    private int currentLineIndex = 0;
    private bool isTyping = true;
    private bool audioPlayed, lineCompleted, endAfterTyping, endEvent = false;

    void Awake()
    {
        SetAiText(angryText,angryAudioClips);
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
                textAudioSource.Stop();
                audioPlayed = true;
                StoryManager.Instance.ResumeTimeline();
                triggered = false;
                return;
            }

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

            lockOutScreen.SetActive(true);
            computerScreen.SetActive(false);
            if(buttonValues[0].value == 8 && buttonValues[1].value == 4 && buttonValues[2].value == 6 && buttonValues[3].value == 2)
            {
                computerScreen.SetActive(true);
                lockOutScreen.SetActive(false);
                systemOverrideSource.Play();
                endEvent = true;
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
    }
}
