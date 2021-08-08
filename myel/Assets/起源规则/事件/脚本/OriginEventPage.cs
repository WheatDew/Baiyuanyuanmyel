using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginEventPage : MonoBehaviour
{
    public Text eventDiscribe;
    public Button buttonPrefab;
    public Transform buttonParent;
    private OriginEffectManager effectManager;

    private void Start()
    {
        effectManager = FindObjectOfType<OriginEffectManager>();
    }

    public void SetEvent(EventData eventData)
    {
        this.eventDiscribe.text = eventData.discribe;
        foreach(var item in eventData.buttonList)
        {
            Button btn = Instantiate(buttonPrefab, buttonParent);
            btn.transform.GetChild(0).GetComponent<Text>().text = item.name;
            btn.onClick.AddListener(delegate
            {

                foreach(var effectItem in item.effectList)
                {
                    print(effectItem.name + " " + effectItem.value);
                    effectManager.effectCommand.Push(
                        new EffectData { name = effectItem.name, value = effectItem.value});
                }
                Destroy(gameObject);
            });
        }
    }
}
