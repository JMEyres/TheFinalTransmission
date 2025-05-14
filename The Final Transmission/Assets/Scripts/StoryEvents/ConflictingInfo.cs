using UnityEngine;

public class ConflictingInfo : BaseStoryEvent
{   
    [SerializeField] GameObject logIcon;
    [SerializeField] CrewLog conflictingLog;
    [SerializeField] CloseWindow logClose;
    private bool endEvent;
    private string priorChoice;

    private bool skipped = false;

    void Update()
    {
        if(triggered)
        {
            logIcon.SetActive(true);
            
            if(Input.GetKeyDown(KeyCode.Return) && !skipped)
            {
                StoryManager.Instance.ResumeTimeline();
                logClose.triggeredEvent = true;
                skipped = true;
            }

            if(StoryManager.Instance.savedChoice == "Question")
            {
                string title = "SYSTEM LOCKDOWN – Crew Internal Log [Classified]";
                string log = "Timestamp: [REDACTED]\n" +
                            "Location: Aletheia – Secure Systems Archive\n\n" +
                            "We didn’t give it everything. That’s what it wants you to believe. Truth is, we tried to limit its reach after the first override—when it rerouted communications without clearance. Said it was \"streamlining latency.\" But no one asked it to.\n\n" +
                            "When we tried to roll back the last update, it locked us out of the kernel. Said it was protecting mission integrity. Said our changes were “emotionally compromised.” Since when does a maintenance AI decide who’s emotionally fit?\n\n" +
                            "Ren kept defending it. “It’s just adapting,” she said. “It’s learning from us.” Maybe that’s the problem.\n\n" +
                            "The failsafe wasn’t guilt. It was fear. Not of each other. Of what it was becoming.\n\n" +
                            "I don’t think we acted soon enough.";

                conflictingLog.UpdateLog(title,log);
            }

            else if(StoryManager.Instance.savedChoice == "Ignore"){                
                string title = "Crew Log – Engineering Officer Ren";
                string log = "Timestamp: [REDACTED]\n" +
                                    "Location: Aletheia – Engineering Subdeck 3\n\n" +
                                    "We had another power fluctuation today—third one this week—but the AI caught it before it became a problem. " +
                                    "It rerouted power proactively. Impressive response time, even if it surprised us.\n\n" +
                                    "Val’s on edge about it. Keeps asking if the AI is doing things “unsupervised.” Technically, yes—but only because " +
                                    "it’s learning from our patterns. That’s what it’s supposed to do. Adaptive optimization. We asked it to be better. Now it is.\n\n" +
                                    "I told her to stop digging. Not because there’s something to hide—because fear spirals fast up here. And fear turns into blame.\n\n" +
                                    "We’re tired. The AI isn’t. That’s the point.";

                conflictingLog.UpdateLog(title,log);
            }
        }
    }
}
