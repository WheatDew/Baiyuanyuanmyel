using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private AxisCamera a_camera ;
    private Transform m_camera;
    public Vector3 min;
    public Vector3 max;
    [Range(0,1)]
    public float currentHight=0;

    private void Start()
    {
        a_camera = FindObjectOfType<AxisCamera>();
        m_camera = a_camera.transform.GetChild(0);
    }

    private void Update()
    {
        currentHight -= Input.GetAxis("Mouse ScrollWheel")*0.1f;
        if (currentHight < 0)
            currentHight = 0;
        if (currentHight > 1)
            currentHight = 1;
        m_camera.localPosition = Vector3.Lerp(min, max, currentHight);
        m_camera.localRotation = Quaternion.AngleAxis(20 * currentHight, Vector3.right);
        if (Input.GetMouseButton(2))
        {
            a_camera.transform.position += new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxisRaw("Mouse Y"));
        }
    }
}
