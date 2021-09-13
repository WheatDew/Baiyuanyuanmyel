using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginRoom : MonoBehaviour
{
    public string Name;
    public Texture paint;
    private MeshRenderer meshRenderer;
    private OriginRoomSystem roomSystem;


    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        roomSystem = FindObjectOfType<OriginRoomSystem>();

        roomSystem.roomEntityLib.Add(Name, this);
        meshRenderer.material.mainTexture = paint;
        gameObject.SetActive(false);
    }
}
