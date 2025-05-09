using UnityEngine;

public class Test : MonoBehaviour
{
    public float audioAmplitude;
    public float timeRemaining = 0f;
    public Material mat;
    // Update is called once per frame
    void Update()
    {
        timeRemaining += Time.deltaTime;
        if (timeRemaining <= 3)
        {
            audioAmplitude = Mathf.Lerp(0.05f, 0.01f, 0.5f);
            mat.SetFloat("_AudioAmplitude", audioAmplitude);
        }
        else if (timeRemaining > 3 && timeRemaining <= 4){
            audioAmplitude = Mathf.Lerp(0.01f, 0.05f, 0.5f);
            mat.SetFloat("_AudioAmplitude", audioAmplitude);
        }
        else{
            timeRemaining = 0f;
        }

    }
}
