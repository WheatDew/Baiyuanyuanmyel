using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightJson;
using System.IO;

public class OriginEventLib : MonoBehaviour
{
    public OriginEventPage eventPagePrefab;
    public Canvas canvas;
    public Dictionary<string, EventData> eventList = new Dictionary<string, EventData>();
    public HashSet<string> currentCondition = new HashSet<string>();
    public string currentWorkString = "";
    public OriginCharacter currentWorkCharacter = null;
    public bool currentWorkFlag=false;
    public float currentWorkTime = 4;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        EventDataLibInitialize();
        StartCoroutine(WorkJob());
    }

    private void Update()
    {
        
    }

    //创建事件页面
    public void CreateEvenetPage(string eventName)
    {
        OriginEventPage eventObj = Instantiate(eventPagePrefab, canvas.transform);
        eventObj.SetEvent(eventList[eventName]);
    }

    //读取文件获取事件信息
    public void EventDataLibInitializeElement(string originStr)
    {

        var json = JsonValue.Parse(originStr);


        foreach(var _event in json["event"].AsJsonArray)
        {
            HashSet<string> conditionSet = new HashSet<string>();

            foreach(var _condition in _event["condition"].AsJsonArray)
            {
                conditionSet.Add(_condition);
            }

            List<EventButtonData> buttonList = new List<EventButtonData>();
            foreach(var _button in _event["button"].AsJsonArray)
            {
                List<EventEffectData> effectList = new List<EventEffectData>();
                foreach(var _effect in _button["effect"].AsJsonArray)
                {
                    effectList.Add(new EventEffectData(_effect["name"], _effect["value"]));
                }
                buttonList.Add(new EventButtonData(_button["name"], effectList));
            }
            eventList.Add(_event["name"], new EventData(_event["name"], _event["probability"], _event["tag"], _event["discribe"], conditionSet, buttonList));
        }
        
    }

    //遍历文件夹下所有文件
    public void EventDataLibInitialize()
    {

        string[] storyFiles = Directory.GetFiles(Application.dataPath + @"\起源规则\事件\事件数据","*.json");

        print(storyFiles.Length);
        foreach(var item in storyFiles)
        {
            EventDataLibInitializeElement(File.ReadAllText(item));
        }

        currentCondition.Add("游戏开始");

        //var files = folder.GetFiles("*.txt");
        //Debug.Log("files count :" + files.Length);
        //for (int i = 0; i < files.Length; i++)
        //{
        //    Debug.Log(files[i].Name);
        //}

    }

    //事件条件触发
    public void EventJob()
    {

        if (currentCondition.Count != 0)
        {
            Dictionary<string, HashSet<string>> tagEvents = new Dictionary<string, HashSet<string>>();
            Dictionary<string, float> tagProbability = new Dictionary<string, float>();
            foreach (var item in eventList)
            {
                if (item.Value.condition.IsSubsetOf(currentCondition))
                {
                    //print("条件判定成功");
                    if (!tagEvents.ContainsKey(item.Value.tag))
                    {
                        tagEvents.Add(item.Value.tag,new HashSet<string> {item.Value.name });
                        tagProbability.Add(item.Value.tag, item.Value.probability);
                    }
                    else
                    {
                        tagEvents[item.Value.tag].Add(item.Value.name);
                        tagProbability[item.Value.tag] += item.Value.probability;
                    }  
                }
            }

            foreach(var item in tagEvents)
            {
                //print(tagProbability[item.Key]);
                float totalProbability = tagProbability[item.Key];
                float curentPointer = Random.Range(0, totalProbability);
                foreach(var eventItem in item.Value)
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

    //添加初始条件
    public void AddStartingCondition()
    {
        currentCondition.Add("游戏开始时");
    }

    IEnumerator WorkJob()
    {
        while (true)
        {
            EventJob();
            yield return null;
        }
    }


    public void WriteCondition(string condition)
    {
        currentCondition.Add(condition);
    }
}


public class EventData
{
    public string name;
    public float probability;
    public string tag;
    public string discribe;
    public HashSet<string> condition = new HashSet<string>();
    public List<EventButtonData> buttonList;
    public EventData(string name,float probability, string tag, string discribe, HashSet<string> condition, List<EventButtonData> buttonList)
    {
        this.name = name;
        this.probability = probability;
        this.tag = tag;
        this.discribe = discribe;
        this.condition = condition;
        this.buttonList = buttonList;
    }
}

public class EventButtonData
{
    public string name;
    public List<EventEffectData> effectList;
    public EventButtonData(string name, List<EventEffectData> effectList)
    {
        this.name = name;
        this.effectList = effectList;
    }
}

public class EventEffectData
{
    public string name;
    public string value;
    public EventEffectData(string name, string value)
    {
        this.name = name;
        this.value = value;
    }
}


