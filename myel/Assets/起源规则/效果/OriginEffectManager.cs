using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginEffectManager : MonoBehaviour
{
    public Stack<EffectData> effectCommand = new Stack<EffectData>();
    private OriginCharacterSelectionSystem selectionSystem;
    private OriginEventLib eventLib;

    private void Start()
    {
        selectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        eventLib = FindObjectOfType<OriginEventLib>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            effectCommand.Push(new EffectData {name="事件跳转",value="摸鱼事件0" });
        }

        while (effectCommand.Count != 0)
        {
            EffectData effectData = effectCommand.Pop();

            ExecuteCommand(effectData.name,effectData.value);
        }

        
    }

    private void ExecuteCommand(string nameStr, string valueStr)
    {
        switch (nameStr)
        {
            case "饥饿值":
                float value=0;
                if (float.TryParse(valueStr, out value))
                {
                    print(value);
                    selectionSystem.targetCharacter.hungerValue += value;
                }
                break;
            case "事件跳转":
                if (eventLib.eventList.ContainsKey(valueStr))
                {
                    eventLib.CreateEvenetPage(valueStr);
                }
                break;
        }
    }
}
