using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private AxisCamera a_cameraPrefab;
    [System.NonSerialized] public AxisCamera a_camera;

    //自定义系统获取
    private OriginCharacterSelectionSystem characterSelectionSystem;

    private Transform child;
    //public Vector3 min, max;
    [Range(0, 1)] public float currentHight;
    public bool isFollowing = true;

    public Camera currentCamera;

    public void Start()
    {
        characterSelectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        a_camera = Instantiate(a_cameraPrefab);
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

        if (isFollowing && characterSelectionSystem.targetCharacter != null)
        {
            a_camera.transform.position = new Vector3(characterSelectionSystem.targetCharacter.transform.position.x
                , a_camera.transform.position.y, characterSelectionSystem.targetCharacter.transform.position.z) ;
        }
    }
}
