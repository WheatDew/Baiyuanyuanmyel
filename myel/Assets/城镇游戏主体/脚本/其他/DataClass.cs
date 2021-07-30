using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataClass
{
    
}

public class BuildingData
{
    public string name;
    public Texture texture;
    public Vector3 scale;

    public BuildingData(string name,Texture texture,Vector3 scale)
    {
        this.name = name;
        this.texture = texture;
        this.scale = scale; 
    }
}
