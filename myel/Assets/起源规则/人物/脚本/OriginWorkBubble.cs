using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginWorkBubble : MonoBehaviour
{
    public SpriteRenderer content;
    [System.NonSerialized] public OriginCharacter originCharacter;

    public void SetContent(Sprite texture)
    {
        content.sprite = texture;
    }

}
