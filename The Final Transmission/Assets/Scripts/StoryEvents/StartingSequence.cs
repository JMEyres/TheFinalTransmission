using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StartingSequence : MonoBehaviour, StoryEvent
{
    public string eventId = "StartingSequence";
    public float triggerTime = 10.0f;
  
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] GameObject ship;

    private bool triggered = false;

    public Vector3 targetPosition = new Vector3(0f, 0f, 5f);
    public float moveSpeed = 0.5f;
    public float rotationSpeed = 50f;
    public float damping = 1.5f;
    public float rotationDamping = 0.5f;
    public float maxSpeed = 2.0f;
    public float settleThreshold = 0.1f;

    private bool hasSettled = false;
    [TextArea] public string fullText;
    public float typeSpeed = 0.05f;

    private float timer = 0f;
    private int charIndex = 0;
    private bool isTyping = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()    
    {
        StoryManager.Instance?.RegisterTarget(eventId, this);
        textUI.text = "";
    }

    public void OnStoryEventTriggered(string id)
    {
        triggered = true;
    }
    
    public float GetTriggerTime()
    {
        return triggerTime;
    }

    void Update()
    {
        if(triggered)
        {
            if(!hasSettled) 
            {
                //StoryManager.Instance.ResumeTimeline();
                //triggered = false;
            

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
                if (!isTyping || charIndex >= fullText.Length)
                return;

                timer += Time.deltaTime;

                if (timer >= typeSpeed)
                {
                    timer = 0f;
                    textUI.text += fullText[charIndex];
                    charIndex++;
                }        
            }     
        }
    }
}
