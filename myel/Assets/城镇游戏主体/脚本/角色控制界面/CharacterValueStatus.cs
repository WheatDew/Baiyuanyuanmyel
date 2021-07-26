using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterValueStatus : MonoBehaviour
{
    public Stack<string> command = new Stack<string>();

    public Dictionary<string, Status> valueData=new Dictionary<string, Status>();
    public Dictionary<string, int> packData = new Dictionary<string, int>();
    private Status statusValue;

    public void Start()
    {
        valueData.Add("饥饿", new Status {value=100,rate=-1f }) ;
        valueData.Add("饮水", new Status { value=100,rate=-2f });
        StartCoroutine(ValueSetRate());
    }

    private void Update()
    {
        Command();
        if (Input.GetKeyDown(KeyCode.P))
        {
            DisplayCurrentPack();
        }
    }

    public void Command()
    {
        while (command.Count != 0)
        {
            AnalysisCommand(command.Pop());
        }
    }

    public void AnalysisCommand(string commandGroup)
    {
        string[] commands = commandGroup.Split(';');
        foreach(var command in commands)
        {
            Debug.Log(command);
            string[] splits = command.Split(',');
            string _type = splits[0];
            string _name = splits[1];
            string _value = splits[2];

            if (_type == "数值增加")
            {
                valueData[_name].ValueGain(float.Parse(_value));
            }
            else if (_type == "背包增加")
            {
                if (packData.ContainsKey(_name))
                    packData[_name] += int.Parse(_value);
                else
                {
                    packData.Add(_name, int.Parse(_value));
                }
            }
            else if (_type == "背包减少")
            {
                packData[_name] -= int.Parse(_value);
            }
        }

        
    }
    
    public void DisplayCurrentStatus()
    {
        foreach(var item in valueData)
        {
            print(item.Key +" "+ item.Value);
        }
    }

    public void DisplayCurrentPack()
    {
        foreach(var item in packData)
        {
            print(item.Key + " " + item.Value);
        }
    }

    IEnumerator ValueSetRate()
    {
        while (true)
        {
            foreach(var item in valueData)
            {
                //Debug.Log(item.Value.value+" "+item.Value.rate);
                item.Value.ValueGain(item.Value.rate);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
