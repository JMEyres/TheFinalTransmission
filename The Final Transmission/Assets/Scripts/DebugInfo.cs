using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentEvent;
    [SerializeField] Slider repBar;
    [SerializeField] TextMeshProUGUI repText;  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentEvent.text = "Current Event: " + StoryManager.Instance.currentEvent;

        repBar.value = StoryManager.Instance.AiReputation;
        repText.text = "AI Rep: " + StoryManager.Instance.AiReputation + "/100";
    }
}
