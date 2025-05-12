using UnityEngine;

public class AngerSystemWipe : BaseStoryEvent
{
    // Want this to be a minigame, maybe a QTE to stop the AI deleting files

    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            Debug.Log("Triggered Anger System Wipe Event");
        }
    }
}
