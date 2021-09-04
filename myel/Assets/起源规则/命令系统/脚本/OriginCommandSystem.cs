using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginCommandSystem : MonoBehaviour
{
    [SerializeField] private OriginCommandPage commandPage;

    private Stack<Command> CommandBuffer = new Stack<Command>();

    private OriginEventSystem eventSystem;
    private OriginKeyboardSystem keyboardSystem;

    private void Start()
    {
        eventSystem = FindObjectOfType<OriginEventSystem>();
        keyboardSystem = FindObjectOfType<OriginKeyboardSystem>();
        keyboardSystem.key_t += DisplayCommandPage;
    }

    public void Update()
    {
        CommandJob();
    }

    //显示命令界面
    public void DisplayCommandPage()
    {
        if (!commandPage.gameObject.activeSelf)
            commandPage.gameObject.SetActive(true);
        else
            commandPage.gameObject.SetActive(false);
    }

    //工作流
    private void CommandJob()
    {
        while (CommandBuffer.Count != 0)
        {
            
            Command originCommand= CommandBuffer.Pop();
            print("开始处理命令" +originCommand.systemType.ToString()+" "+originCommand.commandType.ToString());
            switch (originCommand.systemType)
            {
                case SystemType.Event:
                    eventSystem.PushEventCommand(originCommand);
                    break;
            }
        }
    }

    public void PushCommand(SystemType systemType,int commandType,string commandVlaue)
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

public enum SystemType { Empty, Event };

public class Command
{
    public GameObject target;
    public SystemType systemType;
    public int commandType;
    public string commandValue;

    public Command()
    {
        this.target = null;
        this.systemType = SystemType.Empty;
        this.commandType = 0;
        this.commandValue = "";
    }



    public Command(SystemType systemType,int commandType,string commandValue)
    {
        this.target = null;
        this.systemType = systemType;
        this.commandType = commandType;
        this.commandValue = commandValue;
    }

    public Command(GameObject target,SystemType systemType,int commandType,string commandValue)
    {
        this.target = target;
        this.systemType = systemType;
        this.commandType = commandType;
        this.commandValue = commandValue;
    }
}
