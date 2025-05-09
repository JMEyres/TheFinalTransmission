using TMPro;
using UnityEngine;

public class StartingSequence : MonoBehaviour, StoryEvent
{
    public string eventId = "StartingSequence";
    public float triggerTime = 1.0f;
    public int eventIndex = 0;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] GameObject ship;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()    
    {
        StoryManager.Instance?.RegisterTarget(eventId, this);
    }

    public void OnStoryEventTriggered(string id)
    {
        Debug.Log("Starting sequence triggered");
    }
    
    public float GetTriggerTime()
    {
        return triggerTime;
    }
    public string GetEventID()
    {
        return eventId;
    }
    public int GetEventIndex()
    {
        return eventIndex;
    }
}
