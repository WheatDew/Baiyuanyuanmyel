using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventEditor : MonoBehaviour
{
    public Dictionary<string, EventData> EventList = new Dictionary<string, EventData>();
    public InputField eventName, eventDiscribe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class EventData
{
    public string name;
    public string discribe;
    public HashSet<string> condition = new HashSet<string>();
    public List<EventButtonData> buttonList;
    public EventData(string name,string discribe,HashSet<string> condition,List<EventButtonData> buttonList)
    {
        this.name = name;
        this.discribe = discribe;
        this.condition = condition;
        this.buttonList = buttonList;
    }
}

public class EventButtonData
{
    public string name;
    public List<EventEffectData> effectList;
    public EventButtonData(string name,List<EventEffectData> effectList)
    {
        this.name = name;
        this.effectList = effectList;
    }
}

public class EventEffectData
{
    public string name;
    public string value;
    public EventEffectData(string name,string value)
    {
        this.name = name;
        this.value = value;
    }
}
