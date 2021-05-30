using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureButtonElement : MonoBehaviour
{
    public GameObject targetPrefab;

    public void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
        {
            FindObjectOfType<FurnitureController>().target = Instantiate(targetPrefab);
            FindObjectOfType<FurnitureController>().flag = false;
            targetPrefab.SetActive(false);
        });
    }
}
