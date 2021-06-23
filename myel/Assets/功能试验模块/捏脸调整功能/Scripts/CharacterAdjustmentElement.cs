using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAdjustmentElement : MonoBehaviour
{
    public string targetName;
    private CharacterAdjustmentTarget target;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate {
            foreach(var item in FindObjectsOfType<CharacterAdjustmentTarget>())
            {
                if(targetName==item.targetName)
                {
                    target = item;
                }
            }
            if (target != null)
            {
                FindObjectOfType<CharacterAdjustmentController>().target = target.gameObject;
                target = null;
                FindObjectOfType<CharacterAdjustmentController>().flag = false;
            }
            
        });
    }
}
