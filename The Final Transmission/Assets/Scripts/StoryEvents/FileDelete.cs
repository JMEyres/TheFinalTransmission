using UnityEngine;

public class FileDelete : BaseStoryEvent
{
    // More of a cutscene ig, player catches ai deleting files, maybe force computer onto screen

    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            Debug.Log("Triggered File Delete Event");
        }
    }
}
