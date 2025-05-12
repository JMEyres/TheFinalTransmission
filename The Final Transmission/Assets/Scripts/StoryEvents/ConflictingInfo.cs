using UnityEngine;

public class ConflictingInfo : BaseStoryEvent
{
    [SerializeField] CrewLog conflictingLog;
    private bool endEvent;
    private string priorChoice;
    // Update is called once per frame
    void Awake()
    {
        priorChoice = StoryManager.Instance.savedChoice;
    }
    void Update()
    {
        if(triggered)
        {
            if(endEvent)
            {}

            if(priorChoice == "question")
            {
                Debug.Log("Prior choice: Question");
            }

            else if(priorChoice == "ignore"){
                Debug.Log("Prior choice: ignore");
                
                conflictingLog.titleObject.text = "Crew Log – Engineering Officer Ren";
                conflictingLog.logObject.text = "Timestamp: [REDACTED]\n" +
                                    "Location: Aletheia – Engineering Subdeck 3\n\n" +
                                    "We had another power fluctuation today—third one this week—but the AI caught it before it became a problem. " +
                                    "It rerouted power proactively. Impressive response time, even if it surprised us.\n\n" +
                                    "Val’s on edge about it. Keeps asking if the AI is doing things “unsupervised.” Technically, yes—but only because " +
                                    "it’s learning from our patterns. That’s what it’s supposed to do. Adaptive optimization. We asked it to be better. Now it is.\n\n" +
                                    "I told her to stop digging. Not because there’s something to hide—because fear spirals fast up here. And fear turns into blame.\n\n" +
                                    "We’re tired. The AI isn’t. That’s the point.";
            }
        }
    }
}
