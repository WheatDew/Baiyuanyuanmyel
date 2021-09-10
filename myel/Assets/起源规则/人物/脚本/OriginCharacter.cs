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
    private HashSet<string> EnterArea = new HashSet<string>();
    

    public void GeneralInitialize()
    {
        characterSystem = FindObjectOfType<OriginCharacterSystem>();
        originRaySystem = FindObjectOfType<OriginRaySystem>();
        eventLib = FindObjectOfType<OriginEventLib>();
        effectManager = FindObjectOfType<OriginEffectManager>();
    }

    private void Start()
    {
        GeneralInitialize();
        CharacterWorkInitialize();
        SearchModuleInitialize();
    }

    private void Update()
    {
        CharacterPropertyJob();
        CharacterWorkJob();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        EnterArea.Add(other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        EnterArea.Remove(other.name);
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

    public OriginWorkBubble workBubblePrefab;
    public Transform workBubbleParent;
    public SpriteRenderer workBubble;
    public string currentWork;
    public float currentWorkRate;
    public bool isClosedTrigger = false;
    private HashSet<string> workMap = new HashSet<string>();
    private HashSet<string> recordArea = new HashSet<string>();
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
        //显示图标
        if (currentWork==""&&!(recordArea.Count == EnterArea.Count && recordArea.IsSubsetOf(EnterArea)))
        {
            StartCharacterWorkButton();
        }

        //图标事件的触发
        if (currentWork != "")
        {
            if (workBubbleParent.childCount != 0)
            {
                for (int i = 0; i < workBubbleParent.childCount; i++)
                {
                    Destroy(workBubbleParent.GetChild(i).gameObject);
                }
            }
            if (characterSystem.workTextureLib.ContainsKey(currentWork))
            {
                workBubble.sprite = characterSystem.workTextureLib[currentWork];
                workBubble.transform.parent.gameObject.SetActive(true);
            }
            recordArea.Clear();
        }

        //按下按钮时触发事件
        if (workBubbleParent.childCount != 0
            && Input.GetMouseButtonDown(0))
        {
            if (originRaySystem.clickTag == "CharacterButton")
            {
                print("按下按钮：" + originRaySystem.clickName);
                commandSystem.PushCommand(characterSystem.characterActionButton[originRaySystem.clickName].commands);
            }



            //if ( originRaySystem.clickName == "摸鱼")
            //{
            //    eventLib.currentWorkString = "摸鱼";
            //    eventLib.currentWorkCharacter = this;
            //}
            //else if (originRaySystem.clickName == "出入口")
            //{
            //    recordArea.Clear();
            //    if (workBubbleParent.childCount != 0)
            //    {
            //        for (int i = 0; i < workBubbleParent.childCount; i++)
            //        {
            //            Destroy(workBubbleParent.GetChild(i).gameObject);
            //        }
            //    }
            //    foreach(var item in EnterArea)
            //    {
            //        effectManager.effectCommand.Push(new EffectData { name = "场景跳转", value = item });
            //    }

            //    EnterArea.Clear();

            //}
        }
    }

    private void CharacterWorkMapInitialize()
    {
        workMap.Add("爱的小屋");
        workMap.Add("池塘区域");
        workMap.Add("路灯");
        workMap.Add("路灯2");

        workMap.Add("曙光_平民_仓库入口");
        workMap.Add("曙光_平民_仓库出口");
    }

    //开启工作按钮角色事件
    public void StartCharacterWorkButton()
    {
        if (workBubbleParent.childCount != 0)
        {
            recordArea.Clear();
            for (int i = 0; i < workBubbleParent.childCount; i++)
            {
                Destroy(workBubbleParent.GetChild(i).gameObject);
            }
        }

        HashSet<string> areaIntersect = new HashSet<string>(workMap);
        HashSet<string> workSet = new HashSet<string>();
        areaIntersect.IntersectWith(EnterArea);
        recordArea = new HashSet<string>(EnterArea);

        foreach (var item in areaIntersect)
        {
            if (characterSystem.areaToWorkLib.ContainsKey(item))
            {
                workSet.UnionWith(characterSystem.areaToWorkLib[item]);
                //print(workSet.Count);
            }
        }
        int index = 0;
        foreach (var item in workSet)
        {
            OriginWorkBubble obj = Instantiate(workBubblePrefab, workBubbleParent);
            obj.SetContent(characterSystem.workTextureLib[item]);
            obj.transform.localPosition = new Vector3(index * 2 - workSet.Count / 2, 4.6f, 0);
            obj.name = item;
            obj.originCharacter = this;
            index++;
        }
    }

    //关闭工作按钮
    public void CloseCharacterWorkButton()
    {

    }

    public void CharacterBubbleInitialize()
    {
        workBubble.transform.parent.gameObject.SetActive(false);
        commandSystem = FindObjectOfType<OriginCommandSystem>();

        //workBubble.sprite = characterResource.workTextureLib["摸鱼"];
    }

    private void CharacterWorkInitialize()
    {
        CharacterWorkMapInitialize();
        CharacterBubbleInitialize();
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

