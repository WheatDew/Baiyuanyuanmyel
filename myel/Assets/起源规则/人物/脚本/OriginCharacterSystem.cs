using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OriginCharacterSystem : MonoBehaviour
{
    #region 通用模块

    public HashSet<OriginCharacter> originCharacters = new HashSet<OriginCharacter>();

    private OriginCommandSystem commandSystem;
    private OriginEventSystem eventSystem;
    private OriginEffectManager effectManager;
    private OriginCharacterSelectionSystem selectionSystem;

    private void Start()
    {
        commandSystem = FindObjectOfType<OriginCommandSystem>();
        eventSystem = FindObjectOfType<OriginEventSystem>();
        effectManager = FindObjectOfType<OriginEffectManager>();
        selectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        WorkModuleInitialize();
        CommandStrListInitialize();
    }

    private void Update()
    {

    }

    public void CommandStrListInitialize()
    {
        //添加指令
        

        //添加指令字符串
        commandSystem.strToCommandList.Add("character startdialogue", 
            new Vector2Int((int)SystemType.Character,(int)CharacterCommand.StartDialogue));
        commandSystem.strToCommandList.Add("character startmultipledialogue", 
            new Vector2Int((int)SystemType.Character,(int)CharacterCommand.StartMultipleDialogue));

        //添加效果指令
        effectManager.effectList.Add("饥饿值", delegate (string value)
        {
            if (selectionSystem.targetCharacter != null)
                selectionSystem.targetCharacter.hungerValue += float.Parse(value);
        });
        effectManager.effectList.Add("背包物品", delegate (string value)
        {
            int count = 0;
            string[] packValue = value.Split(' ');
            if (int.TryParse(packValue[1], out count))
            {
                if (selectionSystem.targetCharacter == null)
                    print("目标角色为空");
                selectionSystem.targetCharacter.PackAdd(packValue[0], count);
            }
        });
        
    }

    #endregion

    #region 工作模块

    public Sprite[] workSprite;
    public string[] workNames;

    public Dictionary<string, Sprite> workTextureLib = new Dictionary<string, Sprite>();
    public Dictionary<string, HashSet<string>> areaToWorkLib = new Dictionary<string, HashSet<string>>();
    public Dictionary<string, CharacterActionButton> characterActionButton = new Dictionary<string, CharacterActionButton>();

    public void WorkTextureLibInitialize()
    {
        for(int i = 0; i < workNames.Length; i++)
        {
            workTextureLib.Add(workNames[i], workSprite[i]);
        }
    }

    public void WorkModuleInitialize()
    {
        Stack<Command> commands = new Stack<Command>();
        commands.Push(new Command((int)SystemType.Event, (int)EventCommand.CreateGroup, "摸鱼事件"));
        CharacterActionButton cab = new CharacterActionButton("摸鱼", commands);
        characterActionButton.Add("摸鱼",cab);


        areaToWorkLib.Add("爱的小屋", new HashSet<string> { "出入口" });
        areaToWorkLib.Add("池塘区域", new HashSet<string> {"摸鱼"});
        areaToWorkLib.Add("路灯", new HashSet<string> { "出入口" });
        areaToWorkLib.Add("路灯2", new HashSet<string> { "出入口" });

        areaToWorkLib.Add("曙光_平民_仓库入口", new HashSet<string> { "出入口" });
        areaToWorkLib.Add("曙光_平民_仓库出口", new HashSet<string> { "出入口" });

        WorkTextureLibInitialize();
    }

    #endregion

    #region 人物对话模块

    

    #endregion
}

public enum CharacterCommand { Create,StartDialogue,StartMultipleDialogue};

public class CharacterActionButton
{
    public string buttonName;
    public Stack<Command> commands;

    public CharacterActionButton()
    {
        buttonName = "";
        commands = null;
    }

    public CharacterActionButton(string buttonName,Stack<Command> commands)
    {
        this.buttonName = buttonName;
        this.commands =new Stack<Command>(commands);
    }
}

public class PhysiologyProperty
{
    public string name;
    public float value, rate, standard;

    public PhysiologyProperty(string name,float value,float rate,float standard)
    {
        this.name = name;
        this.value = value;
        this.rate = rate;
        this.standard = standard;
    }
}

public class PsychologyProperty
{
    public string name;
    public float value, rate, standard;

    public PsychologyProperty(string name, float value, float rate, float standard)
    {
        this.name = name;
        this.value = value;
        this.rate = rate;
        this.standard = standard;
    }
}
