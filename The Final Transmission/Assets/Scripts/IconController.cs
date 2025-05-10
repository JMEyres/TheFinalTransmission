using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class IconController : MonoBehaviour
{
    public List<GameObject> logs;
    public GameObject icons;
    public void OnObjectToggled()
    {
        if (logs.Any(obj => obj.activeInHierarchy))
        {
            icons.SetActive(false);
        }
        else
        {
            icons.SetActive(true);
        }
    }
}
