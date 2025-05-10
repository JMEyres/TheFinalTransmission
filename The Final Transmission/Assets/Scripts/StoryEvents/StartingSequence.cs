using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingSequence : BaseStoryEvent
{
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] GameObject textObject;
    [SerializeField] GameObject ship;
    [SerializeField] AudioSource typingAudio;
    public Vector3 targetPosition = new Vector3(0f, 0f, 5f);
    public float moveSpeed = 0.5f;
    public float rotationSpeed = 50f;
    public float damping = 1.5f;
    public float rotationDamping = 0.5f;
    public float maxSpeed = 2.0f;
    public float settleThreshold = 0.1f;
    [TextArea] public List<string> fullText;
    public float typeSpeed = 0.05f;
    private bool hasSettled = false;
    private float timer = 0f;
    private int charIndex = 0;
    private int currentLineIndex = 0;
    private bool isTyping = true;
    private bool lineCompleted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()    
    {
        textUI.text = "";
    }

    void Update()
    {
        if(triggered)
        {
            if(!hasSettled) 
            {
                // Distance to target
                float distance = Vector3.Distance(ship.transform.position, targetPosition);

                // Eased speed: slows down as we get closer
                float easedSpeed = Mathf.Clamp(distance * damping, 0.01f, maxSpeed);
            
                ship.transform.position = Vector3.MoveTowards(ship.transform.position, targetPosition, easedSpeed * Time.deltaTime);

                // Use the same distance for easing
                float rotationEasing = Mathf.Clamp(distance * rotationDamping, 0f, 1f);

                // Slowly decrease rotation speed as we get closer
                float easedRotationSpeed = rotationSpeed * rotationEasing;

                ship.transform.Rotate(
                Mathf.Sin(Time.time) * easedRotationSpeed * Time.deltaTime,
                Mathf.Cos(Time.time * 0.5f) * easedRotationSpeed * Time.deltaTime,
                Mathf.Sin(Time.time * 0.7f) * easedRotationSpeed * Time.deltaTime
                );

                // Check if we're close enough to settle
                if (Vector3.Distance(ship.transform.position, targetPosition) < settleThreshold)
                {
                    ship.transform.position = targetPosition;
                    hasSettled = true;
                }
            }

            else
            {
                textObject.SetActive(true);
                if (!isTyping || currentLineIndex >= fullText.Count)
                    return;

                if (charIndex < fullText[currentLineIndex].Length)
                {
                    timer += Time.deltaTime;

                    if (timer >= typeSpeed)
                    {
                        timer = 0f;
                        textUI.text += fullText[currentLineIndex][charIndex];
                        typingAudio.Play();
                        charIndex++;
                    }
                }
                else if (!lineCompleted)
                {
                    lineCompleted = true;
                    Invoke("NextLine", 2.0f);
                }
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
        }
        else
        {
            textObject.SetActive(false);
            isTyping = false;
            StoryManager.Instance.ResumeTimeline();
            triggered = false;
            SceneManager.LoadScene("MainGame");
        }
    }
}
