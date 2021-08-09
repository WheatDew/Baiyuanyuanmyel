using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginCharacter : MonoBehaviour
{
    
    #region 通用继承

    private void Start()
    {
        GeneralInitialize();
        CharacterWorkInitialize();
    }

    private void Update()
    {
        CharacterPropertyJob();
        CharacterWorkJob();
        //测试
        if (Input.GetKeyDown(KeyCode.F))
        {
            DisplayPack();
        }
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

    #region 通用功能

    //通用功能
    private OriginUserInterfaceController userInterfaceController;
    private CharacterResource characterResource;
    private OriginRaySystem originRaySystem;
    private OriginEventLib eventLib;
    private HashSet<string> EnterArea = new HashSet<string>();

    public void GeneralInitialize()
    {
        userInterfaceController = FindObjectOfType<OriginUserInterfaceController>();
        characterResource = FindObjectOfType<CharacterResource>();
        originRaySystem = FindObjectOfType<OriginRaySystem>();
        eventLib = FindObjectOfType<OriginEventLib>();
    }

    #endregion

    #region 角色属性模块

    //角色属性
    [Range(0f, 100f)] public float hungerValue, hungerRate;
    private HashSet<string> reportflag = new HashSet<string>();

    public void HungerValueRateJob()
    {
        hungerValue -= hungerRate*Time.deltaTime;
    }

    //饥饿播报循环
    public void HungerReportJob()
    {
        if (!reportflag.Contains("肚子饿了")&&hungerValue < 50)
        {
            userInterfaceController.ActivateReportDialogue("肚子饿了");
            reportflag.Add("肚子饿了");
        }
        if (reportflag.Contains("肚子饿了") && hungerValue > 80)
        {
            reportflag.Remove("肚子饿了");
        }
    }

    //食物搜寻播报
    public void FoodSearchReportJob()
    {
        if (!reportflag.Contains("附近有池塘，也许可以抓一些鱼来充饥")
            && EnterArea.Contains("池塘附近判定区域")
            && hungerValue < 50)
        {
            userInterfaceController.ActivateReportDialogue("附近有池塘，也许可以抓一些鱼来充饥");
            reportflag.Add("附近有池塘，也许可以抓一些鱼来充饥");
        }

        if (reportflag.Contains("附近有池塘，也许可以抓一些鱼来充饥")
            && !EnterArea.Contains("池塘附近判定区域"))
        {
            reportflag.Remove("附近有池塘，也许可以抓一些鱼来充饥");
        }
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
    private HashSet<string> workMap = new HashSet<string>();
    private HashSet<string> recordArea = new HashSet<string>();

    //显示工作气泡循环
    private void DisplayWorkBubbleJob()
    {
        //print(workMap.Overlaps(EnterArea).ToString() + " " + currentWork+" "+
        //    (recordArea.Count == EnterArea.Count && recordArea.IsSubsetOf(EnterArea)).ToString());
        if (!(recordArea.Count == EnterArea.Count && recordArea.IsSubsetOf(EnterArea))
            && workMap.Overlaps(EnterArea)
            && currentWork=="")
        {
            print("增加 ");

            HashSet<string> areaIntersect = new HashSet<string>(workMap);
            HashSet<string> workSet = new HashSet<string>();
            areaIntersect.IntersectWith(EnterArea);
            recordArea = new HashSet<string>(EnterArea);

            foreach (var item in areaIntersect)
            {
                print(item);
                if (characterResource.areaToWorkLib.ContainsKey(item))
                {
                    workSet.UnionWith(characterResource.areaToWorkLib[item]);
                    print(workSet.Count);
                }
            }
            int index = 0;
            foreach (var item in workSet)
            {
                OriginWorkBubble obj = Instantiate(workBubblePrefab, workBubbleParent);
                obj.SetContent(characterResource.workTextureLib[item]);
                obj.transform.localPosition = new Vector3(index * 2 - workSet.Count / 2, 4.6f, 0);
                obj.name = item;
                obj.originCharacter = this;
                index++;
            }
        }

        if (!(recordArea.Count == EnterArea.Count && recordArea.IsSubsetOf(EnterArea))
            && !workMap.Overlaps(EnterArea)
            && currentWork == "")
        {
            if (workBubbleParent.childCount != 0)
            {
                print("销毁");
                recordArea.Clear();
                for (int i = 0; i < workBubbleParent.childCount; i++)
                {
                    Destroy(workBubbleParent.GetChild(i).gameObject);
                }
            }

        }

        if (currentWork == "摸鱼")
        {
            if (workBubbleParent.childCount != 0)
            {
                for (int i = 0; i < workBubbleParent.childCount; i++)
                {
                    Destroy(workBubbleParent.GetChild(i).gameObject);
                }
            }
            workBubble.sprite = characterResource.workTextureLib["摸鱼"];
            workBubble.transform.parent.gameObject.SetActive(true);
            recordArea.Clear();
        }

        if (workBubbleParent.childCount != 0
            &&Input.GetMouseButtonDown(0)
            && originRaySystem.clickName=="摸鱼")
        {
            eventLib.currentWorkString = "摸鱼";
            eventLib.currentWorkCharacter = this;
        }

    }

    private void CharacterWorkMapInitialize()
    {
        workMap.Add("池塘区域");
    }

    public void CharacterBubbleInitialize()
    {
        workBubble.transform.parent.gameObject.SetActive(false);
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

    #region 角色动作模块

    //public Rigidbody m_rigidbody;
    //public float multiple;
    //public float jumpMultiple;
    //public bool isGround = false;
    //public string groundName;

    //private CharacterSelectionController selectionController;

    //private void Start()
    //{
    //    selectionController = FindObjectOfType<CharacterSelectionController>();
    //}


    //private void Update()
    //{
    //    if (selectionController.character == gameObject)
    //    {
    //        m_rigidbody.AddForce(Input.GetAxis("Horizontal") * multiple, 0, Input.GetAxis("Vertical") * multiple);

    //        if (isGround && Input.GetKeyDown(KeyCode.Space))
    //        {
    //            m_rigidbody.AddForce(Vector3.up * jumpMultiple);

    //        }

    //        if (!isGround)
    //            m_rigidbody.AddForce(Vector3.down * jumpMultiple * 0.01f);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isGround = true;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isGround = false;
    //    }
    //}


    //private void OnTriggerEnter(Collider other)
    //{
    //    groundName = other.gameObject.name;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    groundName = "";
    //}

    #endregion
}

