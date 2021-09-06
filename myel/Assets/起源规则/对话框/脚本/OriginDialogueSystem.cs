using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OriginDialogueSystem : MonoBehaviour
{
    //通用功能
    private Canvas canvas;
    private OriginEventSystem eventSystem;
    private OriginCommandSystem commandSystem;
    private OriginEffectManager effectManager;

    [SerializeField] private OriginDialogueBox dialoguePrefab;
    [HideInInspector] public OriginDialogueBox dialogue;

    //关闭对话框
    public void EndDialogue()
    {
        Destroy(dialogue.gameObject);

        //添加指令
        commandSystem.executeCommands.Add(new Vector2Int((int)SystemType.Character, (int)CharacterCommand.StartDialogue),
            delegate (Command command) { StartDialogue(command.commandValue); });
        commandSystem.executeCommands.Add(new Vector2Int((int)SystemType.Character, (int)CharacterCommand.StartMultipleDialogue),
            delegate (Command command) { StartMultipleDialogue(command.commandValue); });

        //添加效果
        effectManager.effectList.Add("跳转单个对话", delegate (string value)
        {
            StartDialogue(value);
        });
        effectManager.effectList.Add("跳转对话", delegate (string value)
        {
            StartMultipleDialogue(value);
        });
    }

    //创建单个对话框
    private void StartDialogue(string content)
    {
        if (dialogue == null)
        {
            dialogue = Instantiate(dialoguePrefab, FindObjectOfType<Canvas>().transform);
            dialogue.SetContent(content);
        }
        else
        {
            dialogue.SetContent(content);
        }
    }

    //创建多个对话框的协程外壳
    private void StartMultipleDialogue(string name)
    {
        //StartCoroutine(StartMultipleDialogueCoroutine(name));
    }

    
}
