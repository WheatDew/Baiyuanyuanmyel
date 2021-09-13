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
    public Dictionary<string, OriginRoom> roomEntityLib = new Dictionary<string, OriginRoom>();
    public Dictionary<string, OriginBuilding> buildingEntityLib = new Dictionary<string, OriginBuilding>();

    //预制体
    [SerializeField] private OriginRoom roomPrefab;

    //额外的系统引用
    private OriginCommandSystem commandSystem;

    private void Start()
    {
        //测试数据
        roomLib.Add("爱的小屋", new RoomData("爱的小屋", "爱的小屋"));

        //获取额外系统
        commandSystem = FindObjectOfType<OriginCommandSystem>();

        //添加命令
        //创建房间命令
        commandSystem.strToCommandList.Add("room replace",
            new Vector2Int((int)SystemType.Room, (int)RoomCommand.Create));
        commandSystem.executeCommands.Add(new Vector2Int((int)SystemType.Room, (int)RoomCommand.Replace),
            delegate (Command command) { ReplaceByRoom(command.commandValue); });


        //初始化室内贴图字典
        for (int i=0;i<roomTextureList.Length;i++)
        {
            roomTextureLib.Add(roomTextureNameList[i], roomTextureList[i]);
        }
    }

    //创建房间
    public void CreateRoom(string command)
    {
        //string[] info = command.Split(',');
        //OriginRoom targetRoom = Instantiate(roomPrefab);
        //targetRoom.SetRoom(roomTextureLib[info[1]]);
        //targetRoom.transform.position = new Vector3(float.Parse(info[2]), float.Parse(info[3]), float.Parse(info[4]));
        //targetRoom.transform.localScale = new Vector3(float.Parse(info[5]), float.Parse(info[6]), float.Parse(info[7]));
        
        //roomEntityLib.Add(info[0], targetRoom);
    }

    public void ReplaceByRoom(string command)
    {
        
        string[] info = command.Split(',');
        print(info[0] + " " + info[1]);
        if (buildingEntityLib[info[0]].gameObject.activeSelf)
        {
            
            buildingEntityLib[info[0]].gameObject.SetActive(false);
            roomEntityLib[info[1]].gameObject.SetActive(true);
        }
        else
        {
            buildingEntityLib[info[0]].gameObject.SetActive(true);
            roomEntityLib[info[1]].gameObject.SetActive(false);
        }
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

public class RoomCommandData
{
    public string name;
    public string buttonName;
    public Command command;

    public RoomCommandData(string name,string buttonName,Command command)
    {
        this.name = name;
        this.buttonName = buttonName;
        this.command = command;
    }
}
