using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDataSystem : MonoBehaviour
{
    [SerializeField] Texture[] textureList;
    [SerializeField] string[] nameList;
    [SerializeField] Vector3[] scaleList;

    private Dictionary<string, BuildingData> BuildingDataLib = new Dictionary<string, BuildingData>();

    private void Start()
    {
        for(int i=0;i<nameList.Length;i++)
        {
            BuildingDataLib.Add(nameList[i], new BuildingData(nameList[i],textureList[i],scaleList[i]));
        }
    }

    public BuildingData GetTexture(string name)
    {
        return BuildingDataLib[name];
    }
}
