using TMPro;
using UnityEngine;

public class CodeButtons : MonoBehaviour, Interactable
{
    public int value = 0;
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        text.text = value.ToString();
    }

    public void IncreaseValue()
    {
        value++;
        if(value == 10) value = 0;
        text.text = value.ToString();
    }

    public void Interact()
    {
        IncreaseValue();
    }
}
