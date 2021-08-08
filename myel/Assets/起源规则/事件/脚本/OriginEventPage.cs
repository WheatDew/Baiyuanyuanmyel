using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginEventPage : MonoBehaviour
{
    public Text eventDiscribe;
    public Button buttonPrefab;
    public Transform buttonParent;

    private void Start()
    {

    }

    public void SetEvent(EventData eventData)
    {
        this.eventDiscribe.text = eventData.discribe;
        foreach(var item in eventData.buttonList)
        {
            Button btn = Instantiate(buttonPrefab, buttonParent);
            btn.onClick.AddListener(delegate
            {
                Destroy(gameObject);
            });
        }
    }
}
