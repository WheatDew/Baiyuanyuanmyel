﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyTargetObject : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            Destroy(target);
        });
    }

}
