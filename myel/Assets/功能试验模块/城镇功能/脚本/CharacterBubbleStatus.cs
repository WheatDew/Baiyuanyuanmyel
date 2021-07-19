using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBubbleStatus : MonoBehaviour
{
    public CharacterAction characterAction;
    public SpriteRenderer  bubble,bubbleContent;
    public string actName;
    public Sprite[] textlib;

    private void Update()
    {
        if (actName == "摸鱼")
        {
            if (!bubble.gameObject.activeSelf)
                bubble.gameObject.SetActive(true);
            bubbleContent.sprite = textlib[0];
        }
        else
        {
            bubble.gameObject.SetActive(false);
        }

        CheckFishing();
    }

    public void CheckFishing()
    {
        if (characterAction.groundName == "池塘")
        {
            actName = "摸鱼";
        }
        else if (actName == "摸鱼")
        {
            actName = "";
        }
    }
}
