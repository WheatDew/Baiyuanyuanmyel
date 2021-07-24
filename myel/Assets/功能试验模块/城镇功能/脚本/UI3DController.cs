using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3DController : MonoBehaviour
{
    public bool isMouseOver;
    public string buttonName;
    public CameraController cameraController;
    public string clickButtonName;
    public float timer=0;
    private void Update()
    {
        Ray ray = cameraController.currentCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit result;

        isMouseOver = Physics.Raycast(ray, out result, 100, 1 << 9);
        if (isMouseOver)
            buttonName = result.collider.name;
        else
            buttonName = "";

        if (clickButtonName != "")
        {
            timer+=Time.deltaTime;
            if (timer > 1f)
                clickButtonName = "";
        }

        Button3dClick();
    }

    public void Button3dClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickButtonName = buttonName;
            timer = 0;
        }
    }
}
