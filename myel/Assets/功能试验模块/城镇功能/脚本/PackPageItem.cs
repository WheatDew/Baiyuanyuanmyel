using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackPageItem : MonoBehaviour
{

    public Button btn;
    public Image image;
    public string itemName;
    public Sprite icon;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            FindObjectOfType<PackPage>().AddItemWaiting(itemName);
            image.sprite = icon;
        });
    }
}
