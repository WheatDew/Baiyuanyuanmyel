﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OriginEffectManager : MonoBehaviour
{
    public Stack<EffectData> effectCommand = new Stack<EffectData>();
    private OriginCharacterSelectionSystem selectionSystem;
    private OriginMapSystem mapSystem;
    private OriginEventLib eventLib;

    public Dictionary<string, UnityAction<string>> effectList = new Dictionary<string, UnityAction<string>>();

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
        effectList[nameStr].Invoke(valueStr);

    }

    #region 效果处理函数

 
    #endregion
}

public struct EffectData
{
    public string name;
    public string value;
}
