using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceController : MonoBehaviour
{
    public GameObject[] voiceElement;

    public void PlayVoice(int id)
    {
        Instantiate(voiceElement[id]);
    }

}
