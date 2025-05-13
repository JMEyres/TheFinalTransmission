using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Experimental.GlobalIllumination;

public class Explosions : BaseStoryEvent
{
    [SerializeField] AudioSource explosionsAudioSource, alarmAudioSource, textAudioSource;
    [SerializeField] Light alarmLight;
    [SerializeField] GameObject directionalLight;
    [SerializeField] GameObject textObject;
    [SerializeField] TextMeshProUGUI textUI;
    [TextArea] public List<string> accusedText;
    public List<AudioClip> accusedAudioClips;
    public float flashSpeed = 2f;        // Speed of the pulse
    public float minIntensity = 0f;      // Lowest intensity
    public float maxIntensity = 50f;      // Highest intensity
    public bool isFlashing = true;
    private bool increasing, qte = true;
    private bool audioPlayed, hasPlayedAudio, hasPlayedExplosion, accused, lineCompleted, endAfterTyping, endEvent = false;
    private float qteTimer;

    private List<string> currentText;
    private List<AudioClip> currentClips;
    public float typeSpeed = 0.05f;
    private float timer = 0f;
    private int charIndex = 0;
    private int currentLineIndex = 0;
    private bool isTyping = true;

    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            if(Input.GetKeyDown(KeyCode.Return) || endEvent)
            {
                textObject.SetActive(false);
                isTyping = false;
                alarmAudioSource.Stop();
                textAudioSource.Stop();
                audioPlayed = true;
                StoryManager.Instance.ResumeTimeline();
                if(accused) StoryManager.Instance.TriggerEvent("LockOut");
                else {
                    StoryManager.Instance.currentIndex++;
                    StoryManager.Instance.TriggerEvent("FileDelete");
                    }
                isFlashing = false;
                alarmLight.intensity = 0;
                triggered = false;
                return;
            }

            if(!hasPlayedExplosion) {explosionsAudioSource.Play(); hasPlayedExplosion = true;}
            
            if (!isFlashing || alarmLight == null || explosionsAudioSource.isPlaying) return;
            directionalLight.SetActive(false);
            float change = flashSpeed * Time.deltaTime;

            if(qte)
            {
                qteTimer += Time.deltaTime;

                if(qteTimer < 3 && qte) // Accuse AI
                {
                    textObject.SetActive(true);
                    textUI.text = "1. Was that you?";
                    if(Input.GetKeyDown(KeyCode.Alpha1)) {
                        accused = true;
                        SetAiText(accusedText, accusedAudioClips);
                        endAfterTyping = true;
                        qte = false;
                        qteTimer = 0f;
                    }
                }
                else{ // run out of time
                    qte = false;
                    endEvent = true;
                }
            }

            if(accused)
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
            }

            if (increasing)
            {
                if (!hasPlayedAudio)
                {
                    alarmAudioSource.Play();
                    hasPlayedAudio = true;
                }
                alarmLight.intensity += change;
                if (alarmLight.intensity >= maxIntensity)
                {
                    alarmLight.intensity = maxIntensity;
                    increasing = false;
                }
            }
            else
            {
                alarmAudioSource.Stop();
                alarmLight.intensity -= change;
                if (alarmLight.intensity <= minIntensity)
                {
                    alarmLight.intensity = minIntensity;
                    increasing = true;
                    hasPlayedAudio = false;
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
