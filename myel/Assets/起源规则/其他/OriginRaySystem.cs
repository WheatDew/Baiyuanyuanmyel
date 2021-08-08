using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginRaySystem : MonoBehaviour
{
    public string clickName;
    public string clickTag;
    public GameObject clickTarget;
    public RaycastHit result;

    private void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out result))
        {
            clickTarget = result.collider.gameObject;
            clickName = clickTarget.name;
            clickTag = clickTarget.tag;
        }
    }
}
