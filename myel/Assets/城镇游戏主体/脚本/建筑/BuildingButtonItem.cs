using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonItem : MonoBehaviour
{
    public Text buttonName;
    private BuildingSystem buildingSystem;
    private CharacterSelectionController selectionController;
    private Button selfButton;

    private void Start()
    {
        buildingSystem = FindObjectOfType<BuildingSystem>();
        selectionController = FindObjectOfType<CharacterSelectionController>();
        selfButton = GetComponent<Button>();
        selfButton.onClick.AddListener(delegate
        {
            buildingSystem.CreateBuilding(buttonName.text, selectionController.character.transform.position);
        });
    }
    public void SetButtonName(string name)
    {
        buttonName.text = name;
    }

    public void SetSelectionController(CharacterSelectionController selectionController)
    {
        this.selectionController = selectionController;
    }
}
