using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
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
            buildingSystem.CreateBuilding("木屋", selectionController.character.transform.position);
        });
    }



}
