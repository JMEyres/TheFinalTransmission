using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour, Interactable
{
    [SerializeField] GameObject tab;
    [SerializeField] List<GameObject> otherTabs;
   public void Interact()
   {
        for(int i = 0; i < otherTabs.Count; i++)
        {
            otherTabs[i].SetActive(false);
        }
        tab.SetActive(true);
   }
}
