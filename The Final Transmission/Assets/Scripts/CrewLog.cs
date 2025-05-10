using TMPro;
using UnityEngine;

public class CrewLog : MonoBehaviour
{
    public TextMeshProUGUI titleObject;
    public TextMeshProUGUI logObject;
    [TextArea] public string title; 
    [TextArea] public string log; 

    [SerializeField] IconController iconController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titleObject.text = title;
        logObject.text = log;
        iconController.OnObjectToggled();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) 
        {
            gameObject.SetActive(false);
            iconController.OnObjectToggled();
        }

    }
}
