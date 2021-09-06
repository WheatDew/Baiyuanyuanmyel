using LightJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum EventCommand { Create,CreateGroup }
public class OriginEventSystem : MonoBehaviour
{
    private Stack<Command> eventCommand = new Stack<Command>();
    [SerializeField] private OriginEventPage eventPagePrefab;
    [SerializeField] private Canvas canvas;

    public Dictionary<string, EventData> eventList = new Dictionary<string, EventData>();
    public Dictionary<string, List<EventData>> eventTagList = new Dictionary<string, List<EventData>>();

    //其他系统的引用
    private OriginCommandSystem commandSystem;
    private OriginEffectManager effectManager;

    private void Start()
    {
        commandSystem = FindObjectOfType<OriginCommandSystem>();
        effectManager = FindObjectOfType<OriginEffectManager>();
        EventDataLibInitialize();
        //添加指令集
        CommandStrListInitialize();
    }

    private void Update()
    {
        EventCommandJob();
    }

    //指令集字符串初始化
    public void CommandStrListInitialize()
    {
        commandSystem.strToCommandList.Add("event create", 
            new Vector2Int((int)SystemType.Event,(int)EventCommand.Create));
        commandSystem.strToCommandList.Add("event creategroup", 
            new Vector2Int((int)SystemType.Event, (int)EventCommand.CreateGroup));

        commandSystem.executeCommands.Add(new Vector2Int((int)SystemType.Event, (int)EventCommand.Create),
            delegate (Command command) { CreateEvenetPage(command.commandValue); });
        commandSystem.executeCommands.Add(new Vector2Int((int)SystemType.Event, (int)EventCommand.CreateGroup),
            delegate (Command command) { CreateEventGroupPage(command.commandValue); });

        //效果指令列表
        effectManager.effectList.Add("事件跳转", delegate (string value) { CreateEvenetPage(value); });

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
                case EventCommand.CreateGroup:
                    CreateEventGroupPage(command.commandValue);
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
            EventData eventData = new EventData(_event["name"], _event["probability"], _event["tag"], _event["discribe"], conditionSet, buttonList);
            eventList.Add(_event["name"], eventData);
            if (eventTagList.ContainsKey(eventData.tag))
            {
                eventTagList[eventData.tag].Add(eventData);
            }
            else
            {
                eventTagList.Add(eventData.tag, new List<EventData>());
                eventTagList[eventData.tag].Add(eventData);
            }
        }

    }

    //创建事件页面
    public void CreateEvenetPage(string eventName)
    {
        OriginEventPage eventObj = Instantiate(eventPagePrefab, canvas.transform);
        eventObj.SetEvent(eventList[eventName]);
    }

    public void CreateEventGroupPage(string eventName)
    {
        Dictionary<string, HashSet<string>> tagEvents = new Dictionary<string, HashSet<string>>();
        Dictionary<string, float> tagProbability = new Dictionary<string, float>();



        foreach (var item in eventTagList[eventName])
        {
            //print("条件判定成功");
            if (!tagEvents.ContainsKey(item.tag))
            {
                tagEvents.Add(item.tag, new HashSet<string> { item.name });
                tagProbability.Add(item.tag, item.probability);
            }
            else
            {
                tagEvents[item.tag].Add(item.name);
                tagProbability[item.tag] += item.probability;
            }

        }

        foreach (var item in tagEvents)
        {
            //print(tagProbability[item.Key]);
            float totalProbability = tagProbability[item.Key];
            float curentPointer = Random.Range(0, totalProbability);
            foreach (var eventItem in item.Value)
            {
                if ((curentPointer -= eventList[eventItem].probability) <= 0)
                {
                    CreateEvenetPage(eventList[eventItem].name);
                    print("生成事件" + eventList[eventItem].name);
                    break;
                }
            }

        }
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

