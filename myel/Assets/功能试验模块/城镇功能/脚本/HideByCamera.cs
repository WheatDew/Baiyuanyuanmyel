using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideByCamera : MonoBehaviour
{
    private CameraController cameraController;
    private MeshRenderer meshRenderer;
    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            if (Vector3.Distance(cameraController.currentCamera.transform.position, transform.position) < 10)
                meshRenderer.enabled = false;
            else
                meshRenderer.enabled = true;
        }
        
    }
}
