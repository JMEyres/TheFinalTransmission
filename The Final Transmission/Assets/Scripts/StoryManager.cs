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
        Cursor.lockState = CursorLockMode.Locked;
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
        currentIndex++;
    }
    public void PauseTimeline()
    {
        timelinePaused = true;
    }

    public void SkipNextEvent()
    {
        if(currentIndex == 0) SceneManager.LoadScene("MainGame");
        ResumeTimeline();
        timer=0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) SkipNextEvent();

        if(timelinePaused) return;

        timer+=Time.deltaTime;
        
        if(storyEventRecievers[timeline[currentIndex]].GetTriggerTime() == 0)
        {
            PauseTimeline();
        }

        while (currentIndex < timeline.Count && timer >= storyEventRecievers[timeline[currentIndex]].GetTriggerTime())
        {
            PauseTimeline();
            TriggerEvent(timeline[currentIndex]);
            timer=0;
        }
    }

}
