using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginPackComponent : MonoBehaviour
{
    public Transform createTransform;

    public void OnDisable()
    {
        for(int i = 0; i < createTransform.childCount; i++)
        {
            Destroy(createTransform.GetChild(i).gameObject);
        }
    }
}
