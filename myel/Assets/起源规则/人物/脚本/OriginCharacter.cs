using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginCharacter : MonoBehaviour
{

    #region 通用功能


    //自定义类型
    private OriginCharacterSystem characterSystem;
    private OriginRaySystem originRaySystem;
    private OriginEventLib eventLib;
    private OriginEffectManager effectManager;

    //描述
    public string realName;
    
    private void Start()
    {


        GeneralInitialize();
        CharacterWorkInitialize();
        SearchModuleInitialize();

        characterSystem.characterList.Add(realName, this);
    }



    private void Update()
    {
        CharacterPropertyJob();
        CharacterWorkJob();
        if (Input.GetKeyDown(KeyCode.C))
        {
            DisplayPack();
        }
    }

    public void GeneralInitialize()
    {
        characterSystem = FindObjectOfType<OriginCharacterSystem>();
        originRaySystem = FindObjectOfType<OriginRaySystem>();
        eventLib = FindObjectOfType<OriginEventLib>();
        effectManager = FindObjectOfType<OriginEffectManager>();
    }

    #endregion

    #region 角色属性模块

    //角色属性
    [Range(0f, 100f)] public float hungerValue, hungerRate;
    private HashSet<string> reportflag = new HashSet<string>();
    public Dictionary<string, PhysiologyProperty> physiologyProperty = new Dictionary<string, PhysiologyProperty>();

    public void AddPhysiologyProperty(string name,float value,float rate,float standrad)
    {
        if (!physiologyProperty.ContainsKey(name))
        {
            physiologyProperty.Add(name, new PhysiologyProperty(name, value, rate, standrad));
        }
    }

    public void RemovePhysiologyProperty(string name)
    {
        if (physiologyProperty.ContainsKey(name))
            physiologyProperty.Remove(name);
    }

    public void HungerValueRateJob()
    {
        hungerValue -= hungerRate*Time.deltaTime;
        if (hungerValue > 80)
            AddPhysiologyProperty("吃饱喝足", 60, 0.5f, 60);
        if (hungerValue < 50)
            AddPhysiologyProperty("饥饿", 100, 0.5f, 100);

        HashSet<string> removeList = new HashSet<string>();
        foreach(var item in physiologyProperty)
        {
            physiologyProperty[item.Key].value -= item.Value.rate * Time.deltaTime;
            if (item.Value.value < 0)
            {
                removeList.Add(item.Key);
            }
        }

        foreach(var item in removeList)
        {
            RemovePhysiologyProperty(item);
            print("删除条目" + item);
        }
    }

    //饥饿播报循环
    public void HungerReportJob()
    {
        //if (!reportflag.Contains("肚子饿了")&&hungerValue < 50)
        //{
        //    userInterfaceController.ActivateReportDialogue("肚子饿了");
        //    reportflag.Add("肚子饿了");
        //}
        //if (reportflag.Contains("肚子饿了") && hungerValue > 80)
        //{
        //    reportflag.Remove("肚子饿了");
        //}
    }

    //食物搜寻播报
    public void FoodSearchReportJob()
    {
        //if (!reportflag.Contains("附近有池塘，也许可以抓一些鱼来充饥")
        //    && EnterArea.Contains("池塘附近判定区域")
        //    && hungerValue < 50)
        //{
        //    userInterfaceController.ActivateReportDialogue("附近有池塘，也许可以抓一些鱼来充饥");
        //    reportflag.Add("附近有池塘，也许可以抓一些鱼来充饥");
        //}

        //if (reportflag.Contains("附近有池塘，也许可以抓一些鱼来充饥")
        //    && !EnterArea.Contains("池塘附近判定区域"))
        //{
        //    reportflag.Remove("附近有池塘，也许可以抓一些鱼来充饥");
        //}
    }

    public void CharacterPropertyJob()
    {
        HungerValueRateJob();
        HungerReportJob();
        FoodSearchReportJob();
    }

    #endregion

    #region 角色工作模块

    public Transform workBubbleParent;
    public SpriteRenderer workBubble;
    public string currentWork;
    public float currentWorkRate;
    public bool isClosedTrigger = false;
    public Dictionary<string, Command> TriggerCommands = new Dictionary<string, Command>();
    private int LastTriggerCommandsCount = 0;

    private Dictionary<string, EffectData[]> buttonEffectList = new Dictionary<string, EffectData[]>();
    private OriginCommandSystem commandSystem;

    //记录角色的行为状态
    private string currentAction="";
    private float currentActionTime=0;

    //更改状态的协程
    private IEnumerator ChangeCurrentAction(string value,float targetTime)
    {
        currentActionTime = 0;
        while (currentActionTime < targetTime)
        {
            yield return new WaitForSeconds(0.5f);
            currentActionTime += 0.5f;
        }
        currentAction = value;
    }

    //显示工作气泡循环
    private void DisplayWorkBubbleJob()
    {
        if (LastTriggerCommandsCount != TriggerCommands.Count)
        {
            DrawWorkButtonByList();
            LastTriggerCommandsCount = TriggerCommands.Count;
        }

    }

    //绘制工作按钮
    public void DrawWorkButtonByList()
    {
        for (int i = 0; i < workBubbleParent.childCount; i++)
        {
            Destroy(workBubbleParent.GetChild(i).gameObject);
        }

        int index = 0;
        foreach (var item in TriggerCommands)
        {
            OriginWorkBubble obj = Instantiate(characterSystem.workBubblePrefab, workBubbleParent);
            obj.SetContent(characterSystem.workTextureLib[item.Key],item.Value,commandSystem);
            obj.transform.localPosition = new Vector3(index * 2 - TriggerCommands.Count / 2, 4.6f, 0);
            obj.name = item.Key;
            obj.originCharacter = this;
            index++;
        }
    }

    private void CharacterWorkInitialize()
    {
        //数据初始化
        workBubble.transform.parent.gameObject.SetActive(false);
        commandSystem = FindObjectOfType<OriginCommandSystem>();
        //绘制气泡初始化
        

    }

    private void CharacterWorkJob()
    {
        DisplayWorkBubbleJob();
    }
    #endregion

    #region 角色背包模块

    public Dictionary<string, int> pack = new Dictionary<string, int>();

    public void PackAdd(string itemName,int count)
    {
        if (pack.ContainsKey(itemName))
        {
            pack[itemName] += count;
        }
        else
        {
            pack.Add(itemName, count);
        }
    }

    public void PackRemove(string itemName,int count)
    {

    }

    //测试：显示背包内物体
    public void DisplayPack()
    {
        string s = "";
        foreach(var item in pack)
        {
            s+=item.Key+" "+item.Value+";";
        }
        print(s);
    }

    #endregion

    #region 角色搜索模块

    public Dictionary<string, SearchData> searchInfoList = new Dictionary<string, SearchData>();

    private void SearchModuleInitialize()
    {
        List<EffectData> effectDatas = new List<EffectData>();
        effectDatas.Add(new EffectData { name = "事件跳转", value = "摸鱼事件0" });
        searchInfoList.Add("树", new SearchData("树","这是一棵树",effectDatas));
    }

    #endregion

    #region 角色心理模块

    public Dictionary<string, PsychologyProperty> psychologyList = new Dictionary<string, PsychologyProperty>();

    //基础函数

    private void CharacterPsychologyPropertyJob()
    {

    }

    private void CharacterPsychologyPropertyInitialize()
    {

    }

    //增加条目
    public void AddPsychologyItem(string name)
    {
        psychologyList.Add(name, new PsychologyProperty(name, 0, 0, 0));
    }

    #endregion

    #region 角色Npc模块

    public bool isNpc;
    

    #endregion

}

