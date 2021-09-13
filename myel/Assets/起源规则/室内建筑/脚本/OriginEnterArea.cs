using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginEnterArea : MonoBehaviour
{
    private string commandName="出入口";
    public string building, room;

    private Command command;

    private void Start()
    {
        command = new Command((int)SystemType.Room, (int)RoomCommand.Replace, string.Format("{0},{1}",building,room));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (commandName!=null&&command!=null&&other.tag == "Character")
        {
            other.GetComponent<OriginCharacter>().TriggerCommands.Add(commandName, command);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (commandName != null && command != null && other.tag == "Character")
        {
            other.GetComponent<OriginCharacter>().TriggerCommands.Remove(commandName);
        }
    }

}
