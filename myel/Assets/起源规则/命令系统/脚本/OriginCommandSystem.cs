using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SystemType { Empty, Event, Character };
public class OriginCommandSystem : MonoBehaviour
{
    [SerializeField] private OriginCommandPage commandPage;

    private Stack<Command> CommandBuffer = new Stack<Command>();

    //指令标准列表
    public Dictionary<Vector2Int, UnityAction<Command>> executeCommands = new Dictionary<Vector2Int, UnityAction<Command>>();
    //指令字符串列表
    public Dictionary<string, Vector2Int> strToCommandList = new Dictionary<string, Vector2Int>(); 

    private void Start()
    {
        OriginCommandInvoke();
    }

    public void Update()
    {
        CommandJob();
        CommandPageJob();
    }

    //起源命令触发
    public void OriginCommandInvoke()
    {
        if(OriginLargeMapPublicData.publicMapCommands!=null)
        foreach(var item in OriginLargeMapPublicData.publicMapCommands)
        {
            PushCommand(item);
        }
    }

    //命令界面显示工作流
    public void CommandPageJob()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            DisplayCommandPage();
    }

    //显示命令界面
    public void DisplayCommandPage()
    {
        if (!commandPage.gameObject.activeSelf)
        {
            commandPage.gameObject.SetActive(true);
            OriginKeyboardSystem.isAction = false;
        }
        else
        {
            commandPage.gameObject.SetActive(false);
            OriginKeyboardSystem.isAction = true;
        }

    }

    //工作流
    private void CommandJob()
    {
        while (CommandBuffer.Count != 0)
        {
            Command originCommand= CommandBuffer.Pop();
            print("开始处理命令" +originCommand.systemType.ToString()+" "+originCommand.commandType.ToString());
            executeCommands[new Vector2Int(originCommand.systemType,originCommand.commandType)].Invoke(originCommand);
        }
    }

    public void PushCommand(int systemType,int commandType,string commandVlaue)
    {
        CommandBuffer.Push(new Command(systemType,commandType,commandVlaue));
    }
    public void PushCommand(Command command)
    {
        CommandBuffer.Push(command);
    }

    //TODO: 这里是深拷贝，未来可以修改一下
    public void PushCommand(Stack<Command> commands)
    {
        Command[] excuteCommands = commands.ToArray();
        foreach(var item in excuteCommands)
        {
            CommandBuffer.Push(item);
        }
    }
}



public class Command
{
    public GameObject target;
    public int systemType;
    public int commandType;
    public string commandValue;

    public Command()
    {
        this.target = null;
        this.systemType = 0;
        this.commandType = 0;
        this.commandValue = "";
    }



    public Command(int systemType,int commandType,string commandValue)
    {
        this.target = null;
        this.systemType = systemType;
        this.commandType = commandType;
        this.commandValue = commandValue;
    }

    public Command(GameObject target,int systemType,int commandType,string commandValue)
    {
        this.target = target;
        this.systemType = systemType;
        this.commandType = commandType;
        this.commandValue = commandValue;
    }
}
