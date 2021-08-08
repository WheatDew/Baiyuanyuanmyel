using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginDialogueBox : MonoBehaviour
{
    [SerializeField] private Text content;

    public void SetContent(string content)
    {
        this.content.text = content;
    }
}
