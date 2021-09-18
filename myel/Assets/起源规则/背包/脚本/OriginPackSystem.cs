using LightJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginPackSystem : MonoBehaviour
{
    [SerializeField] private OriginPackComponent packPrefab;
    [System.NonSerialized] public OriginPackComponent pack;
    [SerializeField] private OriginPackItemComponent itemPrefab;

    public Sprite[] itemSprites;
    public string[] itemSpriteNames;
    private Dictionary<string, Sprite> itemSpriteList = new Dictionary<string, Sprite>();
    public Dictionary<string, OriginPackItemComponent> currentItemList = new Dictionary<string, OriginPackItemComponent>();
    private Dictionary<string, Stack<EffectData>> itemEffectList = new Dictionary<string, Stack<EffectData>>();
    private Dictionary<string, string> itemDescribeList = new Dictionary<string, string>();

    private Canvas canvas;
    private OriginCharacterSelectionSystem characterSelectionSystem;
    private OriginEffectManager effectManager;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        characterSelectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        effectManager = FindObjectOfType<OriginEffectManager>();
        for(int i = 0; i < itemSprites.Length; i++)
        {
            itemSpriteList.Add(itemSpriteNames[i], itemSprites[i]);
        }

        ItemDataLibInitialize();

        //itemEffectList.Add("大鱼", new Stack<EffectData>());
        //itemEffectList["大鱼"].Push(new EffectData { name = "饥饿值", value = "40" });
    }

    private void Update()
    {
        if (pack!=null&&characterSelectionSystem.targetCharacter)
        {
            UpdataByCharacter();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (pack == null)
                OpenPackPage();
            else
                ClosedPackPage();
        }
    }

    public void PackButton()
    {
        if (pack == null)
            OpenPackPage();
        else
            ClosedPackPage();
    }

    public void OpenPackPage()
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

    public void ClosedPackPage()
    {
        Destroy(pack.gameObject);
        currentItemList.Clear();
        pack = null;
    }

    //创建背包物体
    public void CreateItem(string name,int count)
    {
        var packPageItem = Instantiate(itemPrefab, pack.createTransform);
        packPageItem.effectMananger = effectManager;
        packPageItem.canvas = canvas;
        packPageItem.count.text = count.ToString();
        if (itemSpriteList.ContainsKey(name))
            packPageItem.image.sprite = itemSpriteList[name];
        if (itemEffectList.ContainsKey(name))
            packPageItem.effectDatas = new Stack<EffectData>(itemEffectList[name]);
        if (itemDescribeList.ContainsKey(name))
            packPageItem.describe = itemDescribeList[name];
        currentItemList.Add(name, packPageItem);
    }

    public void SetItem(string name,int count)
    {
        //print(currentItemList[name]==null);
        currentItemList[name].count.text = count.ToString();
    }

    //根据当前角色刷新背包物体
    public void UpdataByCharacter()
    {
        if (pack != null)
        {
            string s = currentItemList.Count.ToString() + " " + characterSelectionSystem.targetCharacter.pack.Count.ToString();
            //if (s!="1 1")
            //print(s);
            foreach (var item in characterSelectionSystem.targetCharacter.pack)
            {
                if (currentItemList.ContainsKey(item.Key))
                {
                    SetItem(item.Key, item.Value);
                }
                else
                {
                    CreateItem(item.Key, item.Value);
                }
            }
        }
        
    }

    //读取文件获取背包物品信息
    public void ItemDataLibInitialize()
    {
        string originStr = System.IO.File.ReadAllText(Application.dataPath + @"\起源规则\背包\数据\物品数据.json");

        var json = JsonValue.Parse(originStr);


        foreach (var _item in json["item"].AsJsonArray)
        {

            Stack<EffectData> effectList = new Stack<EffectData>();
            foreach (var _effect in _item["effect"].AsJsonArray)
            {
                effectList.Push(new EffectData { name= _effect["name"],value = _effect["value"] });
                print(_effect["name"] + " " + _effect["value"]);
            }
            itemEffectList.Add(_item["name"], effectList);
            itemDescribeList.Add(_item["name"], _item["describe"]);
        }

    }
}
