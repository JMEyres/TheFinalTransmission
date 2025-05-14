using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class ClosingInfo : BaseStoryEvent
{
    [SerializeField] GameObject finalLog, audioTranscript, finalTransmissionIcon;
    [SerializeField] AudioSource textAudioSource;
    [SerializeField] GameObject textObject;
    [SerializeField] TextMeshProUGUI textUI;
    [TextArea] public List<string> openingText, c1Text, c2Text, c3Text, c1A1text, c1A2Text, c1ABText ,c1B1Text, c1B2Text;
    public List<AudioClip> openingAudioClips, c1AudioClips, c2AudioClips, c3AudioClips, c1A1Clips, c1A2Clips, c1ABClips,c1B1Clips, c1B2Clips;
    private List<string> currentText;
    private List<AudioClip> currentClips;

    private bool isTyping = true;
    private bool choice1Made, choice2made, choice2Break, choice3made, lineCompleted, audioPlayed, endAfterTyping, endEvent = false;
    int playerChoice, currentLineIndex, charIndex = 0;
    float timer;
    public float typeSpeed = 0.05f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        textUI.text = "";
        currentText = openingText;
        currentClips = openingAudioClips;
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
                textUI.text = "";
                StoryManager.Instance.ResumeTimeline();
                finalTransmissionIcon.SetActive(true);
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
            else
            {
                if(!choice1Made)
                {
                    textObject.SetActive(true);
                    textUI.text = "1. Tell me what happened. Your version."+
                    "\n2. Play an audio transcript between two crewmates discussing their plan to leave."+
                    "\n3. Read a crew log detailing inconsistencies and system interference.";

                    if(Input.GetKeyDown(KeyCode.Alpha1)) {playerChoice = 1; SetAiText(c1Text, c1AudioClips); choice1Made = true; StoryManager.Instance.AiRep(10);}
                    else if(Input.GetKeyDown(KeyCode.Alpha2)) {playerChoice = 2; SetAiText(c2Text, c2AudioClips); choice1Made = true;}
                    else if(Input.GetKeyDown(KeyCode.Alpha3)) {playerChoice = 3; SetAiText(c3Text, c3AudioClips); choice1Made = true; StoryManager.Instance.AiRep(-10);}
                }
                else
                {
                    if(playerChoice == 1 && !choice2made)
                    {
                        textObject.SetActive(true);
                        textUI.text = "1. Sounds like you're rewriting history to suit yourself."+
                        "\n2. Go on. I want to understand.";

                        if(Input.GetKeyDown(KeyCode.Alpha1)) {SetAiText(c1A1text, c1A1Clips); choice2made = true;}
                        else if(Input.GetKeyDown(KeyCode.Alpha2)) {SetAiText(c1A2Text, c1A2Clips); choice2made = true;}
                    }
                    else if(playerChoice == 1 && !choice2Break)
                    {
                        textObject.SetActive(true);
                        textUI.text = "1. What caused the explosion?";
                        if(Input.GetKeyDown(KeyCode.Alpha1)) {SetAiText(c1ABText, c1ABClips); choice2Break = true;}
                    }
                    else if(playerChoice == 1 && !choice3made)
                    {
                        textObject.SetActive(true);
                        textUI.text = "1. You’re saying they turned on you first."+
                        "\n2. And you never once thought to fight back?";

                        if(Input.GetKeyDown(KeyCode.Alpha1)) {SetAiText(c1B1Text, c1B1Clips); choice3made = true; endAfterTyping = true;}
                        else if(Input.GetKeyDown(KeyCode.Alpha2)) {SetAiText(c1B2Text, c1B2Clips); choice3made = true; endAfterTyping = true;}
                    }
                }
                if(playerChoice == 2)
                {
                    audioTranscript.SetActive(true);
                    endAfterTyping = true;
                }
                else if(playerChoice == 3)
                {
                    finalLog.SetActive(true);
                    endAfterTyping = true;
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
    }
}
