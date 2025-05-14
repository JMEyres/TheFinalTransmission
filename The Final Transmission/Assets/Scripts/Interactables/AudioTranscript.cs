using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTranscript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> textAudioClips;
    public int currentLineIndex = 0;
    public bool audioPlayed = false;
    void OnEnable()
    {
        currentLineIndex = 0;
        audioPlayed = false;
    }

    void OnDisable()
    {
        audioSource.Stop();
    }
    public void Update()
    {
        if(currentLineIndex >= textAudioClips.Count) return;
        else{
            audioSource.resource=textAudioClips[currentLineIndex];

            if(!audioPlayed && textAudioClips.Count != 0) 
            {
                audioSource.Play();
                audioPlayed = true;
            }
            else if(!audioSource.isPlaying)
            {
                currentLineIndex++;
                audioPlayed = false;
            }
        }
    }
}
