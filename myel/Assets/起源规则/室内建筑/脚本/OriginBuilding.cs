using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginBuilding : MonoBehaviour
{
    private OriginRoomSystem roomSystem;

    private void Start()
    {
        roomSystem = FindObjectOfType<OriginRoomSystem>();
        roomSystem.roomOriginLib.Add(name, this);
    }
}
