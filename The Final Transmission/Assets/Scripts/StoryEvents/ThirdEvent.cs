using UnityEngine;

public class ThirdEvent : MonoBehaviour, StoryEvent
{
    public string eventId = "ThirdEvent";
    public float triggerTime = 3.0f;
    public int eventIndex = 2;
    void Start()    
    {
        StoryManager.Instance?.RegisterTarget(eventId, this);
    }

    public void OnStoryEventTriggered(string id)
    {
        Debug.Log("Third event triggered");
        StoryManager.Instance.ResumeTimeline();
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
