using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OriginDialogueBox : MonoBehaviour
{
    [SerializeField] private Text content;

    public Text eventDiscribe;
    public Button buttonPrefab;
    public Transform buttonParent;
    private OriginEffectManager effectManager;
    private OriginEventSystem eventSystem;

    private void Start()
    {
        effectManager = FindObjectOfType<OriginEffectManager>();
    }

    public void SetContent(string content)
    {
        this.content.text = content;
    }

    public void SetEvent(EventData eventData)
    {
        this.eventDiscribe.text = eventData.discribe;
        foreach (var item in eventData.buttonList)
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
                Destroy(gameObject);
            });
        }
    }

    //创建多个对话框协程函数
    private IEnumerator StartMultipleDialogueCoroutine(string name)
    {
        string[] subList = eventSystem.eventList[name].discribe.Split('\n');
        int i = 0;
        SetContent(subList[i]);
        i++;
        while (i <= subList.Length)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                if (i != subList.Length)
                {
                    SetContent(subList[i]);
                    i++;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            yield return null;
        }
    }

}
