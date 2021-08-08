using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuildingComponent : MonoBehaviour
{
    public HashSet<string> buildingList = new HashSet<string>();

    private void Start()
    {
        buildingList.Add("木屋");
    }
}
