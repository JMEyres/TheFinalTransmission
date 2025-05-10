using UnityEngine;

public class Folder : MonoBehaviour, Interactable
{
    public GameObject folder;
    public IconController iconController;

    public void Interact()
    {
        folder.SetActive(true);
        iconController.OnObjectToggled();
    }
}
