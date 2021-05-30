using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetMouseButton(2)){
            transform.localPosition += transform.localRotation * (Vector3.up * Input.GetAxis("Mouse Y"));
            transform.localPosition += transform.localRotation * (Vector3.right * Input.GetAxis("Mouse X"));
        }
        transform.localPosition += transform.localRotation * (Vector3.forward * Input.GetAxis("Mouse ScrollWheel")*2);
        if (Input.GetMouseButton(1))
        {
            transform.localRotation=Quaternion.Euler(
                transform.localRotation.eulerAngles.x - Input.GetAxis("Mouse Y"),
                transform.localRotation.eulerAngles.y + Input.GetAxis("Mouse X"),0);
        }
    }
}
