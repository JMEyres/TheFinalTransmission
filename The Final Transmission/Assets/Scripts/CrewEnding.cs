using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class CrewEnding : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject textObject;
    [SerializeField] private TextMeshProUGUI textUI, currentEvent;
    [TextArea] public List<string> goodRepText, badRepText;
    private List<string> currentText;
    public List<AudioClip> goodRepAudio, badRepAudio;
    private List<AudioClip> currentClips;
    [SerializeField] private AudioSource audioSource;

    public float typeSpeed = 0.05f;
    private float timer = 0f;
    private int charIndex = 0;
    private int currentLineIndex = 0;
    private bool isTyping = true;
    private bool lineCompleted, audioPlayed = false;
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

        if(StoryManager.Instance.AiReputation >=50 ) { SetAiText(goodRepText, goodRepAudio); currentEvent.text = "Current Event: CrewEnding Good Rep";}
        else{ SetAiText(badRepText, badRepAudio); currentEvent.text = "Current Event: CrewEnding Bad Rep";}
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
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
        
        if(isTyping)
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
    }
}
