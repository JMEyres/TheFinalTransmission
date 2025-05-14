using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class AIEnding : MonoBehaviour
{
   [SerializeField] GameObject player, mainShip;
   [SerializeField] ParticleSystem playerPS, mainShipPS;
    [SerializeField] GameObject textObject;
    [SerializeField] private TextMeshProUGUI textUI, currentEvent;
    [TextArea] public List<string> goodRepText, badRepText;
    private List<string> currentText;
    public List<AudioClip> goodRepAudio, badRepAudio;
    private List<AudioClip> currentClips;
    [SerializeField] private AudioSource audioSource, alarmAudioSource;

    public float typeSpeed = 0.05f;
    private float timer, alarmTimer = 0f;
    private int charIndex = 0;
    private int currentLineIndex = 0;
    private bool isTyping = true;
    private bool audioPlayed, hasPlayedAudio, hasPlayedExplosion, accused, lineCompleted, badRep = false;
    public float moveSpeed = 1f;
    public float moveDistance = 10f;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float movedDistance = 0f;
    private Vector3 lastPosition;
    private bool isMoving = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = player.transform.position;
        targetPos = startPos + player.transform.forward * moveDistance;

        if(StoryManager.Instance.AiReputation >=50 ) { SetAiText(goodRepText, goodRepAudio); currentEvent.text = "Current Event: AIEnding Good Rep";}
        else{ SetAiText(badRepText, badRepAudio); currentEvent.text = "Current Event: AIEnding Bad Rep"; badRep = true;}
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && !badRep)
        {
            player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // Accumulate distance moved
            float frameDistance = Vector3.Distance(player.transform.position, lastPosition);
            movedDistance += frameDistance;
            lastPosition = player.transform.position;

            if (movedDistance >= moveDistance)
            {
                isMoving = false;
            }
        }
        
        if (isTyping)
        {
            textObject.SetActive(true);

            // Set audio if it's the start of this line
            if (currentLineIndex == 0 && currentClips.Count != 0)
            {
                audioSource.resource = currentClips[currentLineIndex];
            }

            // Trigger alarm audio at a specific audio line
            if (currentLineIndex == 7 && badRep && !alarmAudioSource.isPlaying)
            {
                alarmAudioSource.Play();
            }

            // Only run text typing if this line has associated text
            if (currentLineIndex < currentText.Count)
            {
                if (charIndex < currentText[currentLineIndex].Length)
                {
                    if (!audioPlayed && currentClips.Count != 0)
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
            else // No more text, just audio
            {
                textObject.SetActive(false);

                if (!audioPlayed && currentClips.Count != 0)
                {
                    audioSource.Play();
                    if(currentLineIndex == 10) 
                    {
                        alarmAudioSource.Stop();
                        mainShipPS.Play();
                        playerPS.Play();
                        mainShip.SetActive(false);
                        player.SetActive(false);
                    }
                    audioPlayed = true;
                    Invoke("NextLine", currentClips[currentLineIndex].length + 0.5f); // optional delay
                }
            }
        }
    }
    public void NextLine()
    {
        if (currentLineIndex < currentClips.Count - 1)
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
            alarmAudioSource.Stop();
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
    }
}
