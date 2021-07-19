using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterValueStatus : MonoBehaviour
{
    public Dictionary<string, float> valueData=new Dictionary<string, float>();
    public Stack<string> waitingItem=new Stack<string>();

    public float hungerValue;

    public void Start()
    {
        valueData.Add("饥饿", 100);
    }

    private void Update()
    {
        valueData["饥饿"] -= Time.deltaTime;
        hungerValue = valueData["饥饿"];

        JWaitingItem();
    }
    
    public void JWaitingItem()
    {
        if(waitingItem.Count!=0)
        {
            string itemName = waitingItem.Pop();
            if (itemName == "鱼")
            {
                valueData["饥饿"] += 10;
            }
        }
    }
}
