using LightJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum EventCommand { Create }
public class OriginEventSystem : MonoBehaviour
{
    private Stack<Command> eventCommand = new Stack<Command>();
    [SerializeField] private OriginEventPage eventPagePrefab;
    [SerializeField] private Canvas canvas;

    public Dictionary<string, EventData> eventList = new Dictionary<string, EventData>();

    private void Start()
    {
        EventDataLibInitialize();
    }

    private void Update()
    {
        EventCommandJob();
    }

    //事件命令工作流
    public void EventCommandJob()
    {
        while (eventCommand.Count != 0)
        {
            Command command = eventCommand.Pop();
            print("开始处理命令" + command.systemType.ToString() + " " + command.commandType.ToString());
            switch ((EventCommand)command.commandType)
            {
                case EventCommand.Create:
                    CreateEvenetPage(command.commandValue);
                    break;
            }
        }

    }


    //添加命令
    public void PushEventCommand(Command command)
    {
        eventCommand.Push(command);
        
    }

    //遍历文件夹下所有文件
    public void EventDataLibInitialize()
    {

        string[] storyFiles = Directory.GetFiles(Application.dataPath + @"\起源规则\事件\事件数据", "*.json");

        print(storyFiles.Length);
        foreach (var item in storyFiles)
        {
            EventDataLibInitializeElement(File.ReadAllText(item));
        }

    }

    //读取文件获取事件信息
    public void EventDataLibInitializeElement(string originStr)
    {

        var json = JsonValue.Parse(originStr);


        foreach (var _event in json["event"].AsJsonArray)
        {
            HashSet<string> conditionSet = new HashSet<string>();

            foreach (var _condition in _event["condition"].AsJsonArray)
            {
                conditionSet.Add(_condition);
            }

            List<EventButtonData> buttonList = new List<EventButtonData>();
            foreach (var _button in _event["button"].AsJsonArray)
            {
                List<EventEffectData> effectList = new List<EventEffectData>();
                foreach (var _effect in _button["effect"].AsJsonArray)
                {
                    effectList.Add(new EventEffectData(_effect["name"], _effect["value"]));
                }
                buttonList.Add(new EventButtonData(_button["name"], effectList));
            }
            eventList.Add(_event["name"], new EventData(_event["name"], _event["probability"], _event["tag"], _event["discribe"], conditionSet, buttonList));
            //print("添加事件" + _event["name"]);
        }

    }

    //创建事件页面
    public void CreateEvenetPage(string eventName)
    {
        OriginEventPage eventObj = Instantiate(eventPagePrefab, canvas.transform);
        eventObj.SetEvent(eventList[eventName]);
    }
}

public class EventProbabilityData
{
    public float probability;
    public string eventName;

    public EventProbabilityData(string eventName,float probability)
    {
        this.probability = probability;
        this.eventName = eventName;
    }
}

