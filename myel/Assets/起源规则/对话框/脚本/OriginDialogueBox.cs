using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OriginDialogueBox : MonoBehaviour
{
    [SerializeField] private Text content;
    public Button buttonPrefab;
    public Transform buttonParent;
    private OriginEffectManager effectManager;
    private OriginEventSystem eventSystem;

    public void SetDialogue(string dialogueName)
    {
        StopAllCoroutines();
        StartCoroutine(DialogueCoroutine(dialogueName));
    }


    //创建多个对话框协程函数
    public IEnumerator DialogueCoroutine(string name)
    {
        effectManager = FindObjectOfType<OriginEffectManager>();
        eventSystem = FindObjectOfType<OriginEventSystem>();
        string[] subList = eventSystem.eventList[name].discribe.Split('\n');
        int i = 0;
        content.text = subList[i];
        i++;
        while (i <= subList.Length)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                if (i != subList.Length)
                {
                    content.text = subList[i];
                    i++;
                }
                else
                {
                    if(eventSystem.eventList[name].buttonList.Count==0)
                    Destroy(gameObject);
                    else
                    {
                        foreach (var item in eventSystem.eventList[name].buttonList)
                        {
                            Button btn = Instantiate(buttonPrefab, buttonParent);
                            btn.transform.GetChild(0).GetComponent<Text>().text = item.name;
                            btn.onClick.AddListener(delegate
                            {

                                foreach (var effectItem in item.effectList)
                                {
                                    //print(effectItem.name + " " + effectItem.value);
                                    effectManager.effectCommand.Push(
                                        new EffectData { name = effectItem.name, value = effectItem.value });
                                }

                                for(int n = 0; n < buttonParent.childCount; n++)
                                {
                                    Destroy(buttonParent.GetChild(n).gameObject);
                                }
                            });
                        }
                        break;
                    }
                }
            }
            yield return null;
        }
    }

}
