using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginSearchSystem : MonoBehaviour
{
    private OriginCharacterSelectionSystem SelectionSystem;
    private OriginRaySystem raySystem;
    private OriginEffectManager effectManager;

    private void Start()
    {
        SelectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        raySystem = FindObjectOfType<OriginRaySystem>();
        effectManager = FindObjectOfType<OriginEffectManager>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0)
            &&SelectionSystem.targetCharacter != null
            &&SelectionSystem.targetCharacter.physiologyProperty.ContainsKey("探索模式")
            &&raySystem.clickName!=null
            && SelectionSystem.targetCharacter.searchInfoList.ContainsKey(raySystem.clickName))
        {
            print("触发事件");
            foreach(var item in SelectionSystem.targetCharacter.searchInfoList[raySystem.clickName].effects)
            {
                effectManager.effectCommand.Push(item);
            }
        }
    }
}

public class SearchData
{
    public string name;
    public string describe;
    public List<EffectData> effects;

    public SearchData(string name,string describe,List<EffectData> effects)
    {
        this.name = name;
        this.describe = describe;
        this.effects = effects;
    }
}
