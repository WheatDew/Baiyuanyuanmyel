using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    //需要场景中有对应的ui
    public DialogueComponent c_Dialogue;
    // Start is called before the first frame update
    void Start()
    {
        c_Dialogue.characterName.text = "";
        c_Dialogue.content.text = "";
        c_Dialogue.gameObject.SetActive(false);
    }

    public void SetDialogueSingle(string content,string characterName="")
    {
        c_Dialogue.content.text = content;
        c_Dialogue.characterName.text = characterName;
    }
}
