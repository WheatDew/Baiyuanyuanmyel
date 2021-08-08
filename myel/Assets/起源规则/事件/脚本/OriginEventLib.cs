using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightJson;

public class OriginEventLib : MonoBehaviour
{
    public OriginEventPage eventPagePrefab;
    public Canvas canvas;
    public Dictionary<string, EventData> eventList = new Dictionary<string, EventData>();
    public HashSet<string> currentCondition = new HashSet<string>();
    public string currentWorkString = "";
    public float currentWorkTime = 2;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        EventDataLibInitialize();
        StartCoroutine(WorkStringCircle());
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
    public void EventDataLibInitialize()
    {
        string originStr = System.IO.File.ReadAllText(Application.dataPath + @"\起源规则\事件\事件数据\事件数据.txt");

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
            eventList.Add(_event["name"],new EventData(_event["name"], _event["discribe"],conditionSet, buttonList));
        }
        
    }

    public void EventJob()
    {
        if (currentCondition.Count != 0)
            foreach (var item in eventList)
            {
                print(item.Value.condition.Count);
                if (item.Value.condition.IsSubsetOf(currentCondition))
                {
                    CreateEvenetPage(item.Value.name);
                    foreach(var condition in item.Value.condition)
                    {
                        currentCondition.Remove(condition);
                    }
                }
            }
    }

    IEnumerator WorkJob()
    {
        while (true)
        {
            EventJob();
            yield return null;
        }
    }

    IEnumerator WorkStringCircle()
    {
        while (true)
        {
            if (currentWorkString != "")
            {
                currentCondition.Add(currentWorkString + "开始");
                print(currentWorkString + "开始");
                yield return null;
                currentCondition.Remove(currentWorkString + "开始");
                currentCondition.Add(currentWorkString + "进行");
                print(currentWorkString + "进行");
                yield return new WaitForSeconds(currentWorkTime);
                currentCondition.Remove(currentWorkString + "进行");
                currentCondition.Add(currentWorkString + "结束");
                print(currentWorkString + "结束");
                yield return null;
                currentCondition.Remove(currentWorkString + "结束");
                currentWorkString = "";
                print("收尾");
            }
            yield return null;
        }
    }

    //同步角色工作百分率
    public 

    public void WriteCondition(string condition)
    {
        currentCondition.Add(condition);
    }
}
