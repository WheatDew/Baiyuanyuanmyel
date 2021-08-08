using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGroupPage : MonoBehaviour
{
    public BuildingButtonItem buttonPrefab;
    public Transform parent;
    private CharacterSelectionController selectionController;

    private void OnEnable()
    {
        selectionController = FindObjectOfType<CharacterSelectionController>();
        InitializeBuilingList();
    }

    private void OnDisable()
    {
        for(int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    public void CreateButton(string buildingName)
    {
        BuildingButtonItem obj= Instantiate(buttonPrefab, parent);
        obj.SetButtonName(buildingName);
    }

    public void InitializeBuilingList()
    {
        foreach(var item in selectionController.character.GetComponent<CharacterBuildingComponent>().buildingList)
        {
            CreateButton(item);
        }
    }
}
