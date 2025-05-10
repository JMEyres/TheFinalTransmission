using UnityEngine;

public class GlitchLogTrigger : MonoBehaviour, Interactable
{
    public GameObject window;
    public IconController iconController;
    public void Interact()
    {
        StoryManager.Instance.TriggerEvent("AiGlitch");
        window.SetActive(false);
        iconController.OnObjectToggled();
    }
}
