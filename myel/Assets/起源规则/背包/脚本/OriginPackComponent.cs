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

    public void OnDisable()
    {
        for(int i = 0; i < createTransform.childCount; i++)
        {
            Destroy(createTransform.GetChild(i).gameObject);
        }
        packSystem.currentItemList.Clear();
    }
}
