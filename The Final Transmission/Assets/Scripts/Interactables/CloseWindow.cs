using UnityEngine;

public class CloseWindow : MonoBehaviour, Interactable
{
    public GameObject window;
    public IconController iconController;
    private bool triggeredEvent = false;

    public void Interact()
    {
        window.SetActive(false);
        if(gameObject.name == "IncriminatingCrewLog" && !triggeredEvent) { StoryManager.Instance.TriggerEvent("ConflictingInfo"); triggeredEvent = true; }
        iconController.OnObjectToggled();
    }
}
