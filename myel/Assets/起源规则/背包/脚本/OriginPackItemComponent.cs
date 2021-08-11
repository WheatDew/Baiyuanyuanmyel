using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginPackItemComponent : MonoBehaviour
{
    public OriginEffectManager effectMananger;
    public Image image;
    public Text count;
    public Stack<EffectData> effectDatas=new Stack<EffectData>();

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            PushCommand();
        });
    }

    public void PushCommand()
    {
        while (effectDatas.Count != 0)
        {
            effectMananger.effectCommand.Push(effectDatas.Pop());
        }

    }
}
