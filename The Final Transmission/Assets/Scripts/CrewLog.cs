using TMPro;
using UnityEngine;

public class CrewLog : MonoBehaviour
{
    public TextMeshProUGUI titleObject;
    public TextMeshProUGUI logObject;
    [TextArea] public string title; 
    [TextArea] public string log; 
    [SerializeField] IconController iconController;

    private bool triggeredEvent = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        titleObject.text = title;
        logObject.text = log;
        iconController.OnObjectToggled();
    }

    public void UpdateLog(string newTitle, string newLog)
    {
        titleObject.text = newTitle;
        logObject.text = newLog;
    }
}
