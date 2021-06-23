using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceController : MonoBehaviour
{
    public GameObject[] voiceElement;

    private void Update()
    {
        RaycastHit result;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out result);
        if (result.collider.tag == "character")
        {
            
        }
    }

}
