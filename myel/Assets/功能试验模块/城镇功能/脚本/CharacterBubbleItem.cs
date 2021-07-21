using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBubbleItem : MonoBehaviour
{
    public SpriteRenderer background, content;
    public Sprite icon_b, icon_c;

    private void Start()
    {
        background.sprite = icon_b;
        content.sprite = icon_c;
    }
}
