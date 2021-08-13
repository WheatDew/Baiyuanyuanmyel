using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginNormalButton : MonoBehaviour
{
    private OriginCharacterSelectionSystem selectionSystem;

    private void Start()
    {
        selectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        GetComponent<Button>().onClick.AddListener(delegate {
            selectionSystem.targetCharacter.physiologyProperty.Remove("探索模式");
        });
    }
}
