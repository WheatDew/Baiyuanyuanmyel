using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private AxisCamera a_camera;
    private Transform child;
    public Vector3 min, max;
    [Range(0, 1)]
    public float currentHight;

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

        child.localPosition = Vector3.Lerp(min, max, currentHight);
        child.localRotation = Quaternion.AngleAxis(20 * (currentHight-0.0f), Vector3.right);
        if (Input.GetMouseButton(2))
            a_camera.transform.position += new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
    }
}
