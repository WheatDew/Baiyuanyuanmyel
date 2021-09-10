using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomCommand { Create,Replace}
public class OriginRoomSystem : MonoBehaviour
{
    [SerializeField] private Texture[] roomTextureList;
    [SerializeField] private string[] roomTextureNameList;
    private Dictionary<string, RoomData> roomLib = new Dictionary<string, RoomData>();  
    public Dictionary<string, Texture> roomTextureLib = new Dictionary<string, Texture>();
    private Dictionary<string, OriginRoom> roomEntityLib = new Dictionary<string, OriginRoom>();
    public Dictionary<string, OriginBuilding> roomOriginLib = new Dictionary<string, OriginBuilding>();

    //预制体
    [SerializeField] private OriginRoom roomPrefab;
    //预制体父节点
    [SerializeField] private Transform parent;

    //额外的系统引用
    private OriginCommandSystem commandSystem;

    private void Start()
    {
        //测试数据
        roomLib.Add("爱的小屋", new RoomData("爱的小屋", "爱的小屋"));

        //获取额外系统
        commandSystem = FindObjectOfType<OriginCommandSystem>();

        //添加命令
        commandSystem.strToCommandList.Add("room create",
            new Vector2Int((int)SystemType.Room, (int)RoomCommand.Create));

        commandSystem.executeCommands.Add(new Vector2Int((int)SystemType.Room, (int)RoomCommand.Create),
            delegate (Command command) { CreateRoom(command.commandValue); });


        //初始化室内贴图字典
        for (int i=0;i<roomTextureList.Length;i++)
        {
            roomTextureLib.Add(roomTextureNameList[i], roomTextureList[i]);
        }
    }

    public void CreateRoom(string command)
    {
        string[] info = command.Split(',');
        OriginRoom targetRoom = Instantiate(roomPrefab, parent);
        targetRoom.SetRoom(roomTextureLib[info[1]]);
        if(info.Length==5)
        targetRoom.transform.position = new Vector3(float.Parse(info[2]), float.Parse(info[3]), float.Parse(info[4]));
        roomEntityLib.Add(info[0], targetRoom);
    }

    public void ReplaceByRoom(string command)
    {

    }
}

public class RoomData
{
    public string name;
    public string textureName;

    public RoomData(string name,string textureName)
    {
        this.name = name;
        this.textureName = textureName;
    }
}
