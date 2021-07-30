using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingComponent : MonoBehaviour
{
    [Range(0,1)] public float progress;
    public Transform buildingPlane;
    public MeshRenderer meshRenderer;
    public TextMesh progressRate;
    public Vector3 scaleData;
    private float lastProgress;

    private void Start()
    {
        buildingPlane.localScale = scaleData;
        buildingPlane.localPosition = Vector3.up * scaleData.z * 5f;
        meshRenderer.material.color = new Color(1, 1, 1, 0.5f + progress / 2f);
        BuildingSystem.SetMaterialRenderingMode(meshRenderer.material, BuildingSystem.RenderingMode.Fade);
        lastProgress = progress;
    }

    private void Update()
    {
        progressRate.text = progress.ToString();
        if (lastProgress!=progress)
        {
            meshRenderer.material.color= new Color(1, 1, 1, 0.5f + progress / 2f);
            if (progress == 1)
            {
                BuildingSystem.SetMaterialRenderingMode(meshRenderer.material, BuildingSystem.RenderingMode.Cutout);
            }
        }

        lastProgress = progress;
    }

    
}
