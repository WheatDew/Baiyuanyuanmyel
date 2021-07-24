using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBubble : MonoBehaviour
{
    public CharacterSelectionController c_characterSelection; 
    public CharacterAction characterAction;
    public CharacterBubbleItem bubblePrefab;
    public GameObject bubbleStatus;
    public SpriteRenderer bubbleWorkStatus;
    public Transform bubblePrefabParent;
    public UI3DController ui3dController;
    public string actName,currentAreaName,currentWorkStatus;
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

        actPairs.Add("池塘", new string[] { "摸鱼", "饮水" });
    }

    private void Update()
    {
        if(currentAreaName != characterAction.groundName&&currentWorkStatus=="")
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

        if (ui3dController.clickButtonName != "")
        {
            bubbleWorkStatus.sprite = spritelib[ui3dController.clickButtonName];
            bubbleStatus.SetActive(true);
            currentWorkStatus = ui3dController.clickButtonName;
            
            ui3dController.clickButtonName = "";

        }

        
        
        
    }

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
