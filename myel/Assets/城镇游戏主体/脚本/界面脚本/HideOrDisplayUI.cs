using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOrDisplayUI : MonoBehaviour
{
    public GameObject target;

    public void SetTargetHideOrDisplay()
    {
        if (target.activeSelf)
        {
            target.SetActive(false);
        }
        else
        {
            target.SetActive(true);
        }
    }
}
