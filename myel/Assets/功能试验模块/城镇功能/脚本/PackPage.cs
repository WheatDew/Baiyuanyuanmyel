using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackPage : MonoBehaviour
{
    public PackPageItem item;
    public Transform m_parent;
    public CharacterValueStatus currentCharacter;

    private void Start()
    {
        item.itemName = "鱼";
        Instantiate(item, m_parent);
    }

    public void AddItemWaiting(string itemName)
    {
        currentCharacter.waitingItem.Push(itemName);
    }
}
