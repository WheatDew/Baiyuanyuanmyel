using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackPageItem : MonoBehaviour
{
    public CharacterValueStatus characterValueStatus;
    public Button btn;
    public Image image;
    public Text itemName;
    public Text count;
    public string command;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            characterValueStatus.command.Push(command);
             count.text= (int.Parse(count.text) - 1).ToString();
        });
    }
}
