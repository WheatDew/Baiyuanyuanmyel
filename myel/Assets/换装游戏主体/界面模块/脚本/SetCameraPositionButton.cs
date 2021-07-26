using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPositionButton : MonoBehaviour
{
    public Vector3 position;
    public Vector3 rotation;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
        {
            Camera.main.gameObject.transform.position = position;
            Camera.main.gameObject.transform.rotation = Quaternion.Euler(rotation);
        });
    }
}
