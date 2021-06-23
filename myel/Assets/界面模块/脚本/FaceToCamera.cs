using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{

    private void Update()
    {
        SetCameraFace();
    }
    public void SetCameraFace()
    {
        Quaternion target= Quaternion.LookRotation(Camera.main.transform.position);
        target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
        transform.rotation = target;
    }
}
