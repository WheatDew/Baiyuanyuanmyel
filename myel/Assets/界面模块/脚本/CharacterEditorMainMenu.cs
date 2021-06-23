using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterEditorMainMenu : MonoBehaviour
{
    public GameObject[] submenuList;

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

        
    }

    public void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject()&&Input.GetMouseButtonDown(0))
        {
            foreach (var item in submenuList)
            {
                item.SetActive(false);
            }
        }
    }
}
