using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OriginKeyboardSystem : MonoBehaviour
{
    public UnityAction key_t;
    private bool isKey_t;

    private void Start()
    {
        key_t += delegate { };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!isKey_t)
            {
                key_t();
                isKey_t = true;
            }
            else
            {
                key_t();
                isKey_t = false;
            }

        }

    }
}
