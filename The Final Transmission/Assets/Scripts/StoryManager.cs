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

    public string savedChoice = "";

    float timer = 0;
    public int currentIndex = 0;
    public bool timelinePaused = false;

    public int AiReputation = 50;
    public string currentEvent = "";

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
        if (timeline[currentIndex] != id)
        {
            Debug.Log($"Event '{id}' is not the current expected event in the timeline.");
            return;
        }
        if (storyEventRecievers.TryGetValue(id, out var receiver))
        {
            receiver.OnStoryEventTriggered(id);
            currentEvent = id;
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
    
    public void AiRep(int rep)
    {
        if(AiReputation + rep <= 100 && AiReputation + rep >= 0) AiReputation += rep;
        else if(AiReputation + rep > 100) AiReputation = 100;
        else if(AiReputation + rep < 0) AiReputation = 0;
        Debug.Log(AiReputation);
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged+=OnActiveSceneChanged;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    void Update()
    {
        if(timelinePaused) return;

        timer+=Time.deltaTime;
        
        float triggerTime = storyEventRecievers[timeline[currentIndex]].GetTriggerTime();

        if (triggerTime == 0f)
        {
            // Manual trigger point: pause the timeline and wait for external trigger
            PauseTimeline();
            return;
        }

        if (timer >= triggerTime)
        {
            PauseTimeline();
            TriggerEvent(timeline[currentIndex]);
            timer = 0f;
        }
    }

    void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainGame")
        {
            ResumeTimeline();
        }
    }

}
