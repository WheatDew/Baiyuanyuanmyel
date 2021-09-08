using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DialogueCommand { Create,StartDialogue}
public class OriginDialogueSystem : MonoBehaviour
{
    private OriginCommandSystem commandSystem;
    private OriginEffectManager effectManager;

    [SerializeField] private OriginDialogueBox dialoguePrefab;
    [HideInInspector] public OriginDialogueBox dialogue;

    private void Start()
    {
        commandSystem = FindObjectOfType<OriginCommandSystem>();
        effectManager = FindObjectOfType<OriginEffectManager>();
        DialogueCommandInit();
    }

    //命令初始化
    public void DialogueCommandInit()
    {

        //添加指令
        commandSystem.executeCommands.Add(new Vector2Int((int)SystemType.Dialogue, (int)CharacterCommand.StartDialogue),
            delegate (Command command) { StartDialogue(command.commandValue); });

        //添加指令转换
        commandSystem.strToCommandList.Add("dialogue startdialogue",
            new Vector2Int((int)SystemType.Dialogue, (int)CharacterCommand.StartDialogue));

        //添加效果
        effectManager.effectList.Add("对话跳转", delegate (string value)
        {
            StartDialogue(value);
        });
    }

    //创建单个对话框
    private void StartDialogue(string content)
    {
        if (dialogue == null)
        {
            dialogue = Instantiate(dialoguePrefab, FindObjectOfType<Canvas>().transform);
            dialogue.SetDialogue(content);
        }
        else
        {
            dialogue.SetDialogue(content);
        }
    }    
}
