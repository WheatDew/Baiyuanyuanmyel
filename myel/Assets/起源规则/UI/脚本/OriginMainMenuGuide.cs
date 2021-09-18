using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginMainMenuGuide : MonoBehaviour
{
    public Button guideButton;
    [SerializeField] private OriginMainMenu mainMenuPrefab;
    [System.NonSerialized] public OriginMainMenu mainMenu;
    private void Start()
    {
        guideButton.onClick.AddListener(delegate
        {
            if (mainMenu == null)
            {
                mainMenu = Instantiate(mainMenuPrefab, FindObjectOfType<Canvas>().transform);
                guideButton.interactable = false;
                guideButton.interactable = true;
            }
            else
            {
                guideButton.interactable = false;
                guideButton.interactable = true;
                Destroy(mainMenu.gameObject);
            }

        });
    }
}
