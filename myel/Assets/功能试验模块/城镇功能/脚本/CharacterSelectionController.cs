using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectionController : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject character;
    public GameObject characterUI;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit result;
            Physics.Raycast(cameraController.currentCamera.ScreenPointToRay(Input.mousePosition),out result);
            
            if (result.collider&&result.collider.tag == "Character")
            {
                character = result.collider.gameObject;
                characterUI.SetActive(true);
            }
            else
            {
                character = null;
                characterUI.SetActive(false);
            }
        }
    }
}
