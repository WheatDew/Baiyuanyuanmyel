using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAdjustmentController : MonoBehaviour
{
    public GameObject target;
    public bool flag = false;

    public void Update()
    {
        if (!flag && target != null && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonUp(0))
            {
                flag = true;
            }
            else if(Input.GetMouseButton(0))
            {
                target.transform.localPosition += transform.localRotation * (Vector3.up * Input.GetAxis("Mouse Y"));
                target.transform.localPosition += transform.localRotation * (Vector3.right * Input.GetAxis("Mouse X"));
            }
        }
    }
}
