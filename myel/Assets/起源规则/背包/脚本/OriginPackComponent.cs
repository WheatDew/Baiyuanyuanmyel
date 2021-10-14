using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginPackComponent : MonoBehaviour
{
    public Transform createTransform;
    private OriginPackSystem packSystem;

    private void Start()
    {
        packSystem = FindObjectOfType<OriginPackSystem>();
        packSystem.UpdataByCharacter();
    }

    

    public void OnDestroy()
    {
        packSystem.currentItemList.Clear();
    }
}
