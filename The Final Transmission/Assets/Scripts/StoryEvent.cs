
using UnityEngine.Events;
public interface StoryEvent
{
    string GetEventID();
    void OnStoryEventTriggered(string eventId);
    float GetTriggerTime();
    int GetEventIndex();
}
