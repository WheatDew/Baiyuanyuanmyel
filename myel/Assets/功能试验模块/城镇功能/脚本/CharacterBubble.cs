using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBubble : MonoBehaviour
{
    public CharacterAction characterAction;
    public CharacterBubbleItem bubblePrefab;
    public Transform bubblePrefabParent;
    public string actName,currentAreaName;
    public Sprite[] texturelib;
    public string[] namelib;
    public Dictionary<string, string[]> actPairs = new Dictionary<string, string[]>();

    private void Start()
    {
        actPairs.Add("池塘", new string[] { "摸鱼", "饮水" });
    }

    private void Update()
    {
        if(currentAreaName != characterAction.groundName)
        {
            if (actPairs.ContainsKey(characterAction.groundName))
            {
                DisplayCurrentSelection(actPairs[characterAction.groundName]);
            }
            else
            {
                for(int i = 0; i < bubblePrefabParent.childCount; i++)
                {
                    Destroy(bubblePrefabParent.GetChild(i).gameObject);
                }
            }
        }
        
        currentAreaName = characterAction.groundName;
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
            bubblePrefab.icon_c = texturelib[index];
            bubblePrefab.transform.localPosition = new Vector3(index * 2 - num.Count / 2 , 4.6f, 0);
            Instantiate(bubblePrefab, bubblePrefabParent);
            index++;
        }
    }
}
