using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginSceneEntity : MonoBehaviour
{
    private OriginCharacterSelectionSystem selectionSystem;
    private MeshRenderer meshRenderer;
    private bool flag = true;
    private bool mouseFlag=false;

    private void Start()
    {
        selectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        if (flag&&mouseFlag&&selectionSystem.targetCharacter != null&&selectionSystem.targetCharacter.physiologyProperty.ContainsKey("探索模式"))
        {
            meshRenderer.material.EnableKeyword("_EMISSION");
            flag = false;
        }
        else if(!flag &&
            (!mouseFlag|| selectionSystem.targetCharacter == null ||
            (selectionSystem.targetCharacter != null&&!selectionSystem.targetCharacter.physiologyProperty.ContainsKey("探索模式"))))
        {
            meshRenderer.material.DisableKeyword("_EMISSION");
            flag = true;
        }

        
    }

    private void OnMouseEnter()
    {
        mouseFlag = true;
    }

    private void OnMouseExit()
    {
        mouseFlag = false;
    }
}
