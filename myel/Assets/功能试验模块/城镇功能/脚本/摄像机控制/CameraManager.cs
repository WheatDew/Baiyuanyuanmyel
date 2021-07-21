using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cameraGroups;


    public void SwitchCamera(int cameraIndex)
    {
        foreach(var item in cameraGroups)
        {
            item.SetActive(false);
        }
        cameraGroups[cameraIndex].SetActive(true);
    }
}
