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
    private Dictionary<string, OriginPackItemComponent> currentItemList = new Dictionary<string, OriginPackItemComponent>();
    private Dictionary<string, Stack<EffectData>> itemEffectList = new Dictionary<string, Stack<EffectData>>();

    private Canvas canvas;
    private OriginCharacterSelectionSystem characterSelectionSystem;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        characterSelectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        for(int i = 0; i < itemSprites.Length; i++)
        {
            itemSpriteList.Add(itemSpriteNames[i], itemSprites[i]);
        }
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

    public void OpenPackPage()
    {
        pack = Instantiate(packPrefab,canvas.transform);
    }

    public void ClosedPackPage()
    {
        Destroy(pack.gameObject);
        currentItemList.Clear();
        pack = null;
    }

    //创建背包物体
    public void CreateItem(string name,int count,Stack<EffectData> effectDatas)
    {
        var packPageItem = Instantiate(itemPrefab, pack.createTransform);
        packPageItem.count.text = count.ToString();
        packPageItem.image.sprite = itemSpriteList[name];
        packPageItem.effectDatas = new Stack<EffectData>(effectDatas);
        currentItemList.Add(name, packPageItem);
    }

    public void SetItem(string name,int count)
    {
        currentItemList[name].count.text = count.ToString();
    }

    //根据当前角色刷新背包物体
    public void UpdataByCharacter()
    {
        foreach(var item in characterSelectionSystem.targetCharacter.pack)
        {
            if (currentItemList.ContainsKey(item.Key))
            {
                SetItem(item.Key, item.Value);
            }
            else
            {
                CreateItem(item.Key, item.Value, new Stack<EffectData>());
            }
        }
    }
}
