﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginPhysiologyPropertyItemComponent : MonoBehaviour
{
    private OriginEffectManager effectMananger;
    public Text itemName;
    public Text count;
    public Stack<EffectData> effectDatas = new Stack<EffectData>();

    private void Start()
    {

        //GetComponent<Button>().onClick.AddListener(delegate
        //{
        //    //PushCommand();
        //});
    }

    public void PushCommand()
    {
        while (effectDatas.Count != 0)
        {
            effectMananger.effectCommand.Push(effectDatas.Pop());
        }

    }
}
