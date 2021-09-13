using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginBuilding : MonoBehaviour
{
    //public OriginTriggerArea triggerArea;

    public string Name;
    //public string wallName;
    //public Vector3 roomPosition;
    //public Vector3 roomScale;

    private void Start()
    {
        FindObjectOfType<OriginRoomSystem>().buildingEntityLib.Add(Name, this);
    }

    //private void Start()
    //{
    //    Vector3 offset = roomPosition;
    //    triggerArea.SetRoomEnterArea(roomName, new Command(gameObject, (int)SystemType.Room, (int)RoomCommand.Replace,
    //        string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", roomName, wallName, offset.x, offset.y, offset.z, roomScale.x, roomScale.y, roomScale.z)));
    //}
}
