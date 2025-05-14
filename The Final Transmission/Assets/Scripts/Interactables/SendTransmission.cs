using UnityEngine;
using UnityEngine.SceneManagement;

public class SendTransmission : MonoBehaviour, Interactable
{
    [SerializeField] string choice;
    public void Interact()
    {
        SceneManager.LoadScene(choice);
    }
}
