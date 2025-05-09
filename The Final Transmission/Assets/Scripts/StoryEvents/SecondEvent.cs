using UnityEngine;

public class SecondEvent : MonoBehaviour, StoryEvent
{
    public string eventId = "SecondEvent";
    public float triggerTime = 2.0f;
    public int eventIndex = 1;
    void Start()    
    {
        StoryManager.Instance?.RegisterTarget(eventId, this);
    }

    public void OnStoryEventTriggered(string id)
    {
        Debug.Log("Second event triggered");
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
