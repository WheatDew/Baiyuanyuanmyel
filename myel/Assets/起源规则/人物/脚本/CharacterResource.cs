using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterResource : MonoBehaviour
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


    public void WorkTextureLibInitialize()
    {
        for(int i = 0; i < workNames.Length; i++)
        {
            workTextureLib.Add(workNames[i], workSprite[i]);
        }
        
    }

    public void WorkModuleInitialize()
    {
        areaToWorkLib.Add("池塘区域", new HashSet<string> {"摸鱼"});
        areaToWorkLib.Add("路灯", new HashSet<string> { "出入口" });
        areaToWorkLib.Add("路灯2", new HashSet<string> { "出入口" });


        WorkTextureLibInitialize();
    }

    #endregion


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
