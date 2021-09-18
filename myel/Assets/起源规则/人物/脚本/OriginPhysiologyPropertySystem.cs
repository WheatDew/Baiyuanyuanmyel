using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginPhysiologyPropertySystem : MonoBehaviour
{
    [SerializeField] private OriginPhysiologyPropertyComponent packPrefab;
    [System.NonSerialized] public OriginPhysiologyPropertyComponent pack;
    [SerializeField] private OriginPhysiologyPropertyItemComponent itemPrefab;

    public Sprite[] itemSprites;
    public string[] itemSpriteNames;
    private Dictionary<string, Sprite> itemSpriteList = new Dictionary<string, Sprite>();
    private Dictionary<string, OriginPhysiologyPropertyItemComponent> currentItemList = new Dictionary<string, OriginPhysiologyPropertyItemComponent>();
    private Dictionary<string, Stack<EffectData>> itemEffectList = new Dictionary<string, Stack<EffectData>>();

    private Canvas canvas;
    private OriginCharacterSelectionSystem characterSelectionSystem;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        characterSelectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        for (int i = 0; i < itemSprites.Length; i++)
        {
            itemSpriteList.Add(itemSpriteNames[i], itemSprites[i]);
        }
    }

    private void Update()
    {
        if (pack != null && characterSelectionSystem.targetCharacter)
        {
            UpdataByCharacter();
        }
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    if (pack == null)
        //        OpenPackPage();
        //    else
        //        ClosedPackPage();
        //}
    }

    public void PhysiologyPropertyButton()
    {
        if (pack == null)
            OpenPhysiologyPropertyPage();
        else
            ClosedPhysiologyPropertyPage();
    }

    public void OpenPhysiologyPropertyPage()
    {
        if (pack == null)
        {
            pack = Instantiate(packPrefab, canvas.transform);
        }
        else
        {
            Destroy(pack.gameObject);
            currentItemList.Clear();
            pack = null;
        }
    }

    public void ClosedPhysiologyPropertyPage()
    {
        Destroy(pack.gameObject);
        currentItemList.Clear();
        pack = null;
    }

    //创建生理条目
    public void CreateItem(string name, float value,float standrad, Stack<EffectData> effectDatas)
    {
        var physiologyItem = Instantiate(itemPrefab, pack.createTransform);
        physiologyItem.itemName.text = name;
        physiologyItem.count.text = value.ToString("F0")+"/"+standrad.ToString("F0");
        physiologyItem.effectDatas = new Stack<EffectData>(effectDatas);
        currentItemList.Add(name, physiologyItem);
    }

    public void SetItem(string name, float value,float standrad)
    {
        currentItemList[name].count.text = value.ToString("F0") + "/" + standrad.ToString("F0");
    }

    //根据当前角色刷新角色生理状态
    public void UpdataByCharacter()
    {
        foreach (var item in characterSelectionSystem.targetCharacter.physiologyProperty)
        {
            if (currentItemList.ContainsKey(item.Key))
            {
                SetItem(item.Key, item.Value.value,item.Value.standard);
            }
            else
            {
                CreateItem(item.Key, item.Value.value,item.Value.standard, new Stack<EffectData>());
            }
        }
    }
}
