using UnityEngine;

public class CloseWindow : MonoBehaviour, Interactable
{
    public GameObject window;
    public IconController iconController;
    public bool triggeredEvent, resumeTimeline = false;

    public void Interact()
    {
        if(window.name == "IncriminatingCrewLog" && !triggeredEvent) { StoryManager.Instance.TriggerEvent("ConflictingInfo"); triggeredEvent = true; }
        if(window.name == "ConflictingLog" && !resumeTimeline) { StoryManager.Instance.ResumeTimeline(); resumeTimeline = true; }
        
        window.SetActive(false);
        iconController.OnObjectToggled();
    }
}
