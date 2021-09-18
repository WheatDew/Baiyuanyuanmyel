using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginPlantSystem : MonoBehaviour
{
    private OriginCommandSystem commandSystem;
    private OriginCharacterSystem characterSystem;

    private void Start()
    {
        //指定场景中的命令系统
        commandSystem = FindObjectOfType<OriginCommandSystem>();
        characterSystem = FindObjectOfType<OriginCharacterSystem>();
        InitializeCommand();
    }

    //向命令系统中添加预设指令
    private void InitializeCommand()
    {
        //创建房间命令
        commandSystem.strToCommandList.Add("plant gather",
            new Vector2Int((int)SystemType.Plant, (int)PlantCommand.Gather));
        commandSystem.executeCommands.Add(new Vector2Int((int)SystemType.Plant, (int)PlantCommand.Gather),
            delegate (Command command) { GatherPlant(command.commandValue); });
    }

    public void GatherPlant(string command)
    {
        string[] info = command.Split(',');
        characterSystem.characterList[info[0]].PackAdd(info[1],1);
    }
}

public enum PlantCommand { Create,Gather}
