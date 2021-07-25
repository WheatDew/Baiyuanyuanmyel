using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageCharacterStatus : MonoBehaviour
{
    public CharacterSelectionController characterSelectionController;
    CharacterValueStatus characterValueStatus;
    public Text hunger,thirsty;

    private void OnEnable()
    {
        characterValueStatus = characterSelectionController.character.GetComponent<CharacterValueStatus>();
    }

    public void Update()
    {
        if (characterSelectionController.character)
        {
            hunger.text = characterValueStatus.valueData["饥饿"].ToString();
            thirsty.text = characterValueStatus.valueData["饮水"].ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        
    }
}
