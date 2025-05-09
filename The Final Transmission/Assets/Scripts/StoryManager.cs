using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    private Dictionary<string, StoryEvent> storyEventRecievers = new();
    public List<string> timeline;

    public static StoryManager Instance { get; private set; }

    float timer = 0;
    int currentIndex = 0;
    private bool timelinePaused = false;

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

    public void ResumeTimeline()
    {
        timelinePaused = false;
    }
    public void PauseTimeline()
    {
        timelinePaused = true;
    }

    void Update()
    {
        if(timelinePaused) return;

        timer+=Time.deltaTime;

        while (currentIndex < timeline.Count && timer >= storyEventRecievers[timeline[currentIndex]].GetTriggerTime())
        {
            PauseTimeline();
            TriggerEvent(timeline[currentIndex]);
            currentIndex++;
            timer=0;
        }
    }

}
