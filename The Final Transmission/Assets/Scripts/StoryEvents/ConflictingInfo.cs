using UnityEngine;

public class ConflictingInfo : BaseStoryEvent
{
    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            Debug.Log("Triggered Conflicting Info Event");
        }
    }
}
