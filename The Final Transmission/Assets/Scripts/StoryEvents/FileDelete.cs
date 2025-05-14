using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FileDelete : BaseStoryEvent
{
    [SerializeField] List<GameObject> logsToDelete;
    [SerializeField] GameObject progressBarObject;
    [SerializeField] Slider progressBar;
    [SerializeField] AudioSource deleteAudio;
    [SerializeField] AudioResource successAudio, cancelAudio;
    [SerializeField] CancelButton cancelButton;
    [SerializeField] TextMeshProUGUI text;

    private float timer;
    private int logIndex = 0;

    private bool endEvent = false;
    // More of a cutscene ig, player catches ai deleting files, maybe force computer onto screen

    void Awake()
    {
        progressBar.maxValue = logsToDelete.Count;
        text.text = "Deleting File ... " + logIndex + "/" + logsToDelete.Count;

    }
    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            if(Input.GetKeyDown(KeyCode.Return) || endEvent)
            {
                deleteAudio.Stop();
                progressBarObject.SetActive(false);
                StoryManager.Instance.ResumeTimeline();
                text.text = "";
                triggered = false;
                return;
            }

            progressBarObject.SetActive(true);
            if(!cancelButton.cancel)
            {
                timer+=Time.deltaTime;

                if(timer > 3 && logIndex < logsToDelete.Count)
                {
                    logsToDelete[logIndex].SetActive(false);
                    logIndex++;
                    if(logIndex > 2) deleteAudio.Play();
                    progressBar.value = logIndex;
                    text.text = "Deleting File ... " + logIndex + "/" + logsToDelete.Count;
                    timer = 0;
                }
                else if(logIndex >= logsToDelete.Count){
                    Debug.Log("Files Deleted Successfully");
                    progressBarObject.SetActive(false);
                    endEvent = true;
                }
            }
            else{
                Debug.Log("Cancelled File Delete");
                deleteAudio.resource = cancelAudio;
                deleteAudio.Play();
                progressBarObject.SetActive(false);
                endEvent = true;
            }
        }
    }
}
