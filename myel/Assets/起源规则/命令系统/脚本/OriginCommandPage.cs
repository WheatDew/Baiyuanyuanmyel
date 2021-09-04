using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginCommandPage : MonoBehaviour
{
    private OriginCommandSystem originCommandSystem;

    [SerializeField] private InputField inputField;

    private void Start()
    {
        originCommandSystem = FindObjectOfType<OriginCommandSystem>();
    }

    public void SendCommand()
    {
        try
        {
            string[] slices = inputField.text.Split(' ');
            Command command = new Command();

            switch (slices[0])
            {
                case "event":
                    command.systemType = SystemType.Event;
                    break;
            }

            switch (slices[1])
            {
                case "create":
                    command.commandType = (int)EventCommand.Create;
                    break;
            }

            command.commandValue = slices[2];
            originCommandSystem.PushCommand(command);
            print("命令发送成功");
        }
        catch(Exception ex)
        {
            print(ex);
            print("命令错误");
        }
    }
}
