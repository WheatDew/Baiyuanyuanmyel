using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginPlantSystem : MonoBehaviour
{
    [SerializeField]private OriginPlant plantPrefab;

    private OriginCommandSystem commandSystem;
    private OriginCharacterSystem characterSystem;
    private OriginCharacterSelectionSystem selectionSystem;

    private HashSet<OriginPlant> plantList = new HashSet<OriginPlant>();

    private void Start()
    {
        //指定场景中的命令系统
        commandSystem = FindObjectOfType<OriginCommandSystem>();
        characterSystem = FindObjectOfType<OriginCharacterSystem>();
        selectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        InitializeCommand();
    }

    //向命令系统中添加预设指令
    private void InitializeCommand()
    {
        //创建植物
        commandSystem.strToCommandList.Add("plant create",
            new Vector2Int((int)SystemType.Plant, (int)PlantCommand.Create));
        commandSystem.executeCommands.Add(new Vector2Int((int)SystemType.Plant, (int)PlantCommand.Create),
            delegate (Command command) { CreatePlant(command.commandValue); });

        //收获植物命令
        commandSystem.strToCommandList.Add("plant gather",
            new Vector2Int((int)SystemType.Plant, (int)PlantCommand.Gather));
        commandSystem.executeCommands.Add(new Vector2Int((int)SystemType.Plant, (int)PlantCommand.Gather),
            delegate (Command command) { GatherPlant(command.commandValue); });

    }

    public void CreatePlant(string command)
    {
        //字符串格式：种植坐标（填“self”时表示当前位置）
        OriginPlant originPlant = Instantiate(plantPrefab);
        plantList.Add(originPlant);
        if (command == "self"&&selectionSystem.targetCharacter!=null)
        {
            originPlant.transform.position = selectionSystem.targetCharacter.transform.position;
        }
    }

    public void GatherPlant(string command)
    {
        string[] info = command.Split(',');
        characterSystem.characterList[info[0]].PackAdd(info[1],1);
    }
}

public enum PlantCommand { Create,Gather}
