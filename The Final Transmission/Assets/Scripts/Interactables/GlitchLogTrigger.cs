using UnityEngine;

public class GlitchLogTrigger : MonoBehaviour, Interactable
{
    public GameObject window;
    public IconController iconController;
    public bool triggeredEvent = false;
    public void Interact()
    {
        if(!triggeredEvent){
            StoryManager.Instance.TriggerEvent("AiGlitch");
            triggeredEvent = true;
            }
        
        window.SetActive(false);
        iconController.OnObjectToggled();
    }
}
