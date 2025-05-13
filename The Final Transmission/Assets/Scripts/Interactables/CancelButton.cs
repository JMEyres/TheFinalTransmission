using UnityEngine;

public class CancelButton : MonoBehaviour, Interactable
{
    public bool cancel = false;
    public void Interact()
    {
        cancel = true;
    }
}
