using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBodySpriteSourceButton : MonoBehaviour
{
    public string characterBodyPartName;
    public Sprite spriteSource;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            FindObjectOfType<CharacterBodySpriteSourceController>().CharacterBodySpriteList[characterBodyPartName].sprite = spriteSource;
        });
    }

}
