using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBodySpriteElement : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<CharacterBodySpriteSourceController>()
            .CharacterBodySpriteList.Add(gameObject.name, GetComponent<SpriteRenderer>());
    }
}
