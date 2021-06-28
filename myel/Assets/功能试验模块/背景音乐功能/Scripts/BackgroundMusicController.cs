using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bgmList;
    public GameObject currentBGM; 

    private void Start()
    {
        currentBGM = Instantiate(bgmList[0]);
    }
}
