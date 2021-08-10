using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginEffectManager : MonoBehaviour
{
    public Stack<EffectData> effectCommand = new Stack<EffectData>();
    private OriginCharacterSelectionSystem selectionSystem;
    private OriginMapSystem mapSystem;
    private OriginEventLib eventLib;

    private void Start()
    {
        selectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        eventLib = FindObjectOfType<OriginEventLib>();
        mapSystem = FindObjectOfType<OriginMapSystem>();
    }

    private void Update()
    {

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
                    if (selectionSystem.targetCharacter == null)
                        print("目标角色为空");
                    selectionSystem.targetCharacter.hungerValue += value;
                }
                break;
            case "事件跳转":
                if (eventLib.eventList.ContainsKey(valueStr))
                {
                    eventLib.CreateEvenetPage(valueStr);
                }
                break;
            case "背包物品":
                int count = 0;
                string[] packValue = valueStr.Split(' ');
                if(int.TryParse(packValue[1],out count))
                {
                    if (selectionSystem.targetCharacter == null)
                        print("目标角色为空");
                    selectionSystem.targetCharacter.PackAdd(packValue[0],count);
                }
                break;
            case "场景跳转":
                SceneSwitch(valueStr);
                break;
        }
    }

    #region 效果处理函数

    //工作按钮效果处理函数
    public void EffectWorkButton(string value)
    {
        if (value == "开启")
        {
            selectionSystem.targetCharacter.StartCharacterWorkButton();
        }
    }

    public void SceneSwitch(string value)
    {
        var valueList= value.Split(' ');
        mapSystem.SetNewScene(value);
    }
    #endregion
}
