using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FurnitureController : MonoBehaviour
{
    public GameObject target;
    public bool flag = false;

    public void Update()
    {
        if (!flag && target != null&& !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(1))
            {
                flag = true;
            }
            else
            {
                RaycastHit result;
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out result);
                if (result.point != Vector3.zero)
                {
                    target.transform.position = result.point;
                    if (!target.activeSelf)
                        target.SetActive(true);
                }
            }
        }
    }
}
