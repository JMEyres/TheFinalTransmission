using UnityEngine;

public class CloseWindow : MonoBehaviour, Interactable
{
    public GameObject window;
    public IconController iconController;
    public void Interact()
    {
        window.SetActive(false);
        iconController.OnObjectToggled();
    }
}
