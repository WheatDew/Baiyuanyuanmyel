using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] submenuList;

    private void Start()
    {

    }

    public void SetSubmenu(int id)
    {

        if (!submenuList[id].activeSelf)
        {
            foreach (var item in submenuList)
            {
                item.SetActive(false);
            }
            submenuList[id].SetActive(true);
        }
        else
        {
            foreach (var item in submenuList)
            {
                item.SetActive(false);
            }
        }


    }

}
