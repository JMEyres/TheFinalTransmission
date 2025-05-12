using UnityEngine;

public class Explosions : BaseStoryEvent
{
    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            Debug.Log("Triggered Explosions");
        }
    }
}
