using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class NeutralEnding : MonoBehaviour
{
    [SerializeField] GameObject player, crewShip, lookTarget;
    public Transform targetPosition; 
    [SerializeField] GameObject textObject;
    [SerializeField] private TextMeshProUGUI textUI;
    [TextArea] public List<string> fullText;
    public List<AudioClip> textAudio;
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
        
        crewShip.transform.position = Vector3.MoveTowards(
            crewShip.transform.position,
            targetPosition.position,
            moveSpeed * Time.deltaTime
        );

        // Look at the lookTarget
        transform.LookAt(lookTarget.transform);

        if(isTyping)
        {
            textObject.SetActive(true);
            if(currentLineIndex == 0 && textAudio.Count != 0)
            {
                audioSource.resource=textAudio[currentLineIndex];
            }

            if (charIndex < fullText[currentLineIndex].Length)
            {
                if(!audioPlayed && textAudio.Count != 0) 
                {
                    audioSource.Play();
                    audioPlayed = true;
                }
                timer += Time.deltaTime;

                if (timer >= typeSpeed)
                {
                    timer = 0f;
                    textUI.text += fullText[currentLineIndex][charIndex];
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
        if (currentLineIndex < fullText.Count - 1)
        {
            currentLineIndex++;
            charIndex = 0;
            textUI.text = "";
            lineCompleted = false;
            audioSource.resource = textAudio[currentLineIndex];
            audioPlayed = false;
        }
        else
        {
            textObject.SetActive(false);
            isTyping = false;
            currentLineIndex = 0;
        }
    }
}
