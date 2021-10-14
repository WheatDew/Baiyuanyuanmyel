using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginPlantArea : MonoBehaviour
{
    public OriginPlant plant;
    private string commandName = "采集";
    public string[] harvest;
    private string commandValue="";

    private void Start()
    {
        foreach(var item in harvest)
        {
            commandValue +=item;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (commandName != null && other.tag == "Character")
        {
            OriginCharacter character = other.GetComponent<OriginCharacter>();
            Command command = new Command((int)SystemType.Plant, (int)PlantCommand.Gather, character.realName+","+ commandValue);
            character.TriggerCommands.Add(commandName, command);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (commandName != null && other.tag == "Character")
        {
            other.GetComponent<OriginCharacter>().TriggerCommands.Remove(commandName);
        }
    }
}
