using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueVoiceComponent : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(DestroySlef());
    }

    public IEnumerator DestroySlef()
    {
        while (true)
        {
            if(!audioSource.isPlaying)
            break;
            yield return null;
        }
        Destroy(gameObject);
    }
}
