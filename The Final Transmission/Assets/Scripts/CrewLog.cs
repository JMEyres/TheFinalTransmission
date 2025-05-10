using TMPro;
using UnityEngine;

public class CrewLog : MonoBehaviour
{
    public TextMeshProUGUI titleObject;
    public TextMeshProUGUI logObject;
    [TextArea] public string title; 
    [TextArea] public string log; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titleObject.text = title;
        logObject.text = log;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
