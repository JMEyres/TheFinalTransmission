
using UnityEngine.Events;
public interface StoryEvent
{
    void OnStoryEventTriggered(string eventId);
    float GetTriggerTime();
}
