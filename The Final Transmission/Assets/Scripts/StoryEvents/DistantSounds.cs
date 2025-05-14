using System.Collections.Generic;
using UnityEngine;

public class DistantSounds : BaseStoryEvent
{
    [SerializeField] List<AudioSource> audioSources;
    [SerializeField] List<float> timings;
    public int index = 0;
    private float timer;
    private bool audioPlayed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                for(int i =0; i< audioSources.Count; i++)
                {
                    if(audioSources[i].isPlaying) audioSources[i].Stop();
                }
                StoryManager.Instance.ResumeTimeline();
                triggered = false;
            }
            timer+=Time.deltaTime;

            if(index < audioSources.Count && timer > timings[index])
            {
                if(!audioSources[index].isPlaying) audioSources[index].Play();
                index++;
                timer = 0;
            }
            else if(index >= audioSources.Count && !audioSources[index-1].isPlaying){
                StoryManager.Instance.ResumeTimeline();
                triggered = false;
            }
        }
    }
}
