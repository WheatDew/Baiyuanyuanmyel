using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginSearchButton : MonoBehaviour
{
    private OriginCharacterSelectionSystem selectionSystem;

    private void Start()
    {
        selectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        GetComponent<Button>().onClick.AddListener(delegate {
            selectionSystem.targetCharacter.physiologyProperty.Add("探索模式",
                new PhysiologyProperty ( "探索模式",  100,  0,  100 ));
        });
    }
}
