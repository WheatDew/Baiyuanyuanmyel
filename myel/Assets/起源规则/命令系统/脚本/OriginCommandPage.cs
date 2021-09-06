using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginCommandPage : MonoBehaviour
{
    private OriginCommandSystem commandSystem;

    [SerializeField] private InputField inputField;

    private void Start()
    {
        commandSystem = FindObjectOfType<OriginCommandSystem>();
    }

    public void SendCommand()
    {
        try
        {
            string[] slices = inputField.text.Split(' ');
            Command command = new Command();
            print(slices[0] + " " + slices[1]);
            Vector2Int commandId = commandSystem.strToCommandList[slices[0] + " " + slices[1]];
            command.systemType = commandId.x;
            command.commandType = commandId.y;

            command.commandValue = slices[2];
            commandSystem.PushCommand(command);
            print("命令发送成功");
        }
        catch(Exception ex)
        {
            print(ex);
            print("命令错误");
        }
    }
}
