using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OriginCharacterSelectionSystem : MonoBehaviour
{
    public OriginCharacter targetCharacter;
    private OriginRaySystem raySystem;

    private void Start()
    {
        raySystem = FindObjectOfType<OriginRaySystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (raySystem.clickTag == "Character")
            {
                targetCharacter = raySystem.clickTarget.GetComponent<OriginCharacter>();
            }
        }
    }
}

public class Pack
{
    
}
