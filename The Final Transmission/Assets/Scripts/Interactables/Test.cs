using UnityEngine;

public class Test : MonoBehaviour, Interactable
{
    public void Interact()
    {
        StoryManager.Instance.TriggerEvent("AiGlitch");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
