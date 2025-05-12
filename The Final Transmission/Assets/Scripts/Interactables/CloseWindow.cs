using UnityEngine;

public class CloseWindow : MonoBehaviour, Interactable
{
    public GameObject window;
    public IconController iconController;
    public bool triggeredEvent = false;

    public void Interact()
    {
        if(window.name == "IncriminatingCrewLog" && !triggeredEvent) { StoryManager.Instance.TriggerEvent("ConflictingInfo"); triggeredEvent = true; }
        else if(window.name == "ConflictingLog" && !triggeredEvent) { StoryManager.Instance.ResumeTimeline(); triggeredEvent = true; }
        
        window.SetActive(false);
        iconController.OnObjectToggled();
    }
}
