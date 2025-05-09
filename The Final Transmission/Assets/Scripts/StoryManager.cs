using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    private Dictionary<string, StoryEvent> storyEventRecievers = new();
    private List<StoryEvent> timeline = new();

    public static StoryManager Instance { get; private set; }

    float timer = 0;
    int currentIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void RegisterTarget(string id, StoryEvent receiver)
    {
        if (!storyEventRecievers.ContainsKey(id))
        {
            storyEventRecievers.Add(id, receiver);
            timeline.Add(receiver);
        }
    }

    public void TriggerEvent(string id)
    {
        if (storyEventRecievers.TryGetValue(id, out var receiver))
        {
            receiver.OnStoryEventTriggered(id);
        }
        else
        {
            Debug.LogWarning($"No receiver found for story event ID: {id}");
        }
    }

    void Update()
    {
        timer+=Time.deltaTime;
        while (currentIndex < timeline.Count && timer >= timeline[currentIndex].GetTriggerTime())
        {
            TriggerEvent(timeline[currentIndex].GetEventID());
            currentIndex++;
        }
    }
}
