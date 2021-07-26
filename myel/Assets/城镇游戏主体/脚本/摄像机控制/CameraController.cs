using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private AxisCamera a_camera;
    private Transform child;
    //public Vector3 min, max;
    [Range(0, 1)]
    public float currentHight;
    public Camera currentCamera;

    public void Start()
    {
        a_camera = FindObjectOfType<AxisCamera>();
        child = a_camera.transform.GetChild(0);
    }

    private void Update()
    {
        currentHight -= Input.GetAxis("Mouse ScrollWheel") * 0.1f;
        if (currentHight > 1)
            currentHight = 1;
        if (currentHight < 0)
            currentHight = 0;
        //float cx = currentHight*17<2? -currentHight*17+2:currentHight*17-2;
        float cx = currentHight * 37-30;
        float cy = Mathf.Pow(cx, 2) * 0.02f + 0.14f * cx;
        child.localPosition = new Vector3(0, cy, cx);
        //child.localPosition = Vector3.Lerp(min, max, currentHight);
        child.localRotation = Quaternion.AngleAxis(Mathf.Pow(cx, 2) * (-0.1f) - 3.6f * cx, Vector3.right);
        if (Input.GetMouseButton(2))
            a_camera.transform.position += new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
    }
}
