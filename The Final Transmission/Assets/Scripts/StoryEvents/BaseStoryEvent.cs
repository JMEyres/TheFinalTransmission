using UnityEngine;

public class BaseStoryEvent : MonoBehaviour, StoryEvent
{
    public string eventId = "";
    public float triggerTime = 10.0f;
    protected bool triggered = false;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Awake()    
    {
        StoryManager.Instance?.RegisterTarget(eventId, this);
    }
    public virtual void OnStoryEventTriggered(string id)
    {
        triggered = true;
    }
    public virtual float GetTriggerTime()
    {
        return triggerTime;
    }
}
