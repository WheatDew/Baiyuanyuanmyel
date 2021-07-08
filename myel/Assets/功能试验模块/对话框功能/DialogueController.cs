using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    //需要场景中有对应的ui
    public DialogueComponent c_Dialogue;
    public VoiceController voiceController;
    public string[] randomDialogue;
    // Start is called before the first frame update
    void Start()
    {
        c_Dialogue.characterName.text = "";
        c_Dialogue.content.text = "";
        c_Dialogue.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit result;
             Debug.Log(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out result));

            //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out result) && result.collider.tag == "Character")
            //    StartCoroutine(SetDailyConversation());
        }

    }

    public IEnumerator SetDailyConversation()
    {
        SetDialogueSingle(randomDialogue[Random.Range(0, randomDialogue.Length)]);
        voiceController.PlayVoice(0);
        yield return new WaitForSeconds(5);

        CloseDialogue();
    }

    public void SetDialogueSingle(string content,string characterName="")
    {
        c_Dialogue.content.text = content;
        c_Dialogue.characterName.text = characterName;
        c_Dialogue.gameObject.SetActive(true);

    }

    public void CloseDialogue()
    {
        c_Dialogue.content.text = "";
        c_Dialogue.characterName.text = "";
        c_Dialogue.gameObject.SetActive(false);
    }
}
