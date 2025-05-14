using UnityEngine;

public class CancelButton : MonoBehaviour, Interactable
{
    public bool cancel = false;
    public AudioSource cancelAudio;
    public void Interact()
    {
        cancel = true;
        cancelAudio.Play();
    }
}
