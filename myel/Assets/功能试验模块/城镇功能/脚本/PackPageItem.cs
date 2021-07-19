using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackPageItem : MonoBehaviour
{

    public Button btn;
    public string itemName;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            FindObjectOfType<PackPage>().AddItemWaiting(itemName);
        });
    }
}
