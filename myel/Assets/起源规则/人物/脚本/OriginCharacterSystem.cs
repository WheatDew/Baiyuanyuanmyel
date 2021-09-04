using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginCharacterSystem : MonoBehaviour
{
    #region 通用模块

    public HashSet<OriginCharacter> originCharacters = new HashSet<OriginCharacter>();

    private void Start()
    {
        WorkModuleInitialize();
    }

    private void Update()
    {
        
    }

    #endregion

    #region 工作模块资源数据

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
        commands.Push(new Command(SystemType.Event, (int)EventCommand.Create, "摸鱼事件1"));
        CharacterActionButton cab = new CharacterActionButton("摸鱼", commands);
        characterActionButton.Add("摸鱼",cab);

        areaToWorkLib.Add("池塘区域", new HashSet<string> {"摸鱼"});
        areaToWorkLib.Add("路灯", new HashSet<string> { "出入口" });
        areaToWorkLib.Add("路灯2", new HashSet<string> { "出入口" });

        areaToWorkLib.Add("曙光_平民_仓库入口", new HashSet<string> { "出入口" });
        areaToWorkLib.Add("曙光_平民_仓库出口", new HashSet<string> { "出入口" });

        WorkTextureLibInitialize();
    }

    #endregion


}

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
