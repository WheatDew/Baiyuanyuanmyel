using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBubble : MonoBehaviour
{
    public CharacterWork characterWork;
    public CharacterSelectionController c_characterSelection; 
    public CharacterAction characterAction;
    public CharacterBubbleItem bubblePrefab;
    public GameObject bubbleStatus;
    public SpriteRenderer bubbleWorkStatus;
    public Transform bubblePrefabParent;
    public UI3DController ui3dController;
    public string actName,currentAreaName;
    public Sprite[] texturelib;
    public string[] namelib;
    public Dictionary<string, Sprite> spritelib = new Dictionary<string, Sprite>();
    public Dictionary<string, string[]> actPairs = new Dictionary<string, string[]>();

    private void Start()
    {
        for(int i = 0; i < namelib.Length; i++)
        {
            spritelib.Add(namelib[i], texturelib[i]);
        }

        actPairs.Add("池塘", new string[] { "摸鱼", "取水" });
    }

    private void Update()
    {
        if(currentAreaName != characterAction.groundName&&characterWork.currentWork=="")
        {
            if (actPairs.ContainsKey(characterAction.groundName))
            {
                DisplayCurrentSelection(actPairs[characterAction.groundName]);
            }
            else
            {
                ClearBubbleParent();
            }
        }
        
        currentAreaName = characterAction.groundName;

        ExecuteClickButtonNameString();
        CheckCharacterIsMoving();
    }

    //如果角色发生了移动则终止当前工作状态
    public void CheckCharacterIsMoving()
    {
        if (characterAction.m_rigidbody.velocity!=Vector3.zero)
        {
            characterWork.currentWork = "";
            bubbleStatus.SetActive(false);
        }
    }

    //选项泡泡的点击字符串的处理
    public void ExecuteClickButtonNameString()
    {
        if (ui3dController.clickButtonName != "")
        {
            bubbleWorkStatus.sprite = spritelib[ui3dController.clickButtonName];
            characterAction.m_rigidbody.velocity = Vector3.zero;
            bubbleStatus.SetActive(true);
            characterWork.currentWork = ui3dController.clickButtonName;
            ClearBubbleParent();
            ui3dController.clickButtonName = "";
        }
    }

    //清除选项泡泡父物体下的所有泡泡选项
    public void ClearBubbleParent()
    {
        for (int i = 0; i < bubblePrefabParent.childCount; i++)
        {
            Destroy(bubblePrefabParent.GetChild(i).gameObject);
        }
    }

    public void DisplayCurrentSelection(params string[] bubbles)
    {
        HashSet<int> num = new HashSet<int>();
        foreach(var item in bubbles)
        {
            for(int i=0;i<namelib.Length;i++)
            {
                if (item == namelib[i])
                {
                    num.Add(i);
                }
            }
        }
        int index = 0;
        foreach(var item in num)
        {
            bubblePrefab.icon_c = texturelib[item];
            bubblePrefab.transform.localPosition = new Vector3(index * 2 - num.Count / 2 , 4.6f, 0);
            CharacterBubbleItem obj= Instantiate(bubblePrefab, bubblePrefabParent);
            obj.name = namelib[item];
            index++;
        }
    }
}
