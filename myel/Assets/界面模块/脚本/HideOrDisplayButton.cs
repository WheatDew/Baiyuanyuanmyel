using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideOrDisplayButton : MonoBehaviour
{
    public GameObject[] HiddenList;
    public GameObject[] DisplayList;
    public bool isSwitched = false;
    private bool flag = true;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {

            if (flag)
            {
                foreach (var item in HiddenList)
                {
                    item.SetActive(false);
                }

                foreach (var item in DisplayList)
                {
                    item.SetActive(true);
                }
            }
            else
            {
                foreach (var item in HiddenList)
                {
                    item.SetActive(true);
                }

                foreach (var item in DisplayList)
                {
                    item.SetActive(false);
                }
            }
            if (isSwitched)
                flag = !flag;
        });
    }
}
