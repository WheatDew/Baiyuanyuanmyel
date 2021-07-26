using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackPage : MonoBehaviour
{
    public PackPageItem item;
    public Transform m_parent;
    public CharacterSelectionController selectionController;
    public Sprite[] sprites;
    private CharacterValueStatus currentCharacter;
    private Dictionary<string, PackPageItem> data = new Dictionary<string, PackPageItem>();
    private Dictionary<string, Sprite> spriteLib = new Dictionary<string, Sprite>();
    private Dictionary<string, string> itemCommandLib = new Dictionary<string, string>();

    private void Start()
    {
        spriteLib.Add("鱼", sprites[0]);
        spriteLib.Add("水", sprites[1]);
        itemCommandLib.Add("鱼", "数值增加,饥饿,20;背包减少,鱼,1");
        itemCommandLib.Add("水", "数值增加,饮水,20;背包减少,水,1");
    }

    private void OnEnable()
    {
        currentCharacter = selectionController.character.GetComponent<CharacterValueStatus>();
    }

    private void Update()
    {
        RefreshPack();
    }

    public void RefreshPack()
    {
        if (currentCharacter != null)
            foreach (var single in currentCharacter.packData)
            {
                if (!data.ContainsKey(single.Key))
                {
                    item.gameObject.name = single.Key;
                    item.image.sprite = spriteLib[single.Key];
                    item.itemName.text = single.Key;
                    item.count.text = single.Value.ToString();
                    item.characterValueStatus = currentCharacter;
                    item.command = itemCommandLib[single.Key];
                    data.Add(single.Key, Instantiate(item, m_parent));
                }
                else
                {
                    data[single.Key].count.text = single.Value.ToString();
                }
            }
    }

    private void OnDisable()
    {
        currentCharacter = null;
    }

    //public void AddItemWaiting(string itemName)
    //{
    //    //currentCharacter.waitingItem.Push(itemName);
    //}
}
