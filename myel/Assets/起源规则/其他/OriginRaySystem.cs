using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginRaySystem : MonoBehaviour
{
    public string clickName;
    public RaycastHit result;

    private void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out result))
        {
            clickName = result.collider.name;
        }
    }
}
