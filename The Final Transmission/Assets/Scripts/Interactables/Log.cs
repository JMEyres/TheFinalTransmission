using UnityEngine;

public class Log : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject crewLog;
    [SerializeField] private GameObject parentFolder;
    public void Interact()
    {
        crewLog.SetActive(true);
        if(parentFolder != null) parentFolder.SetActive(false);
    }
}
