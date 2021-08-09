using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterResource : MonoBehaviour
{

    private void Start()
    {
        WorkModuleInitialize();
    }

    private void Update()
    {
        
    }

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
        WorkTextureLibInitialize();
    }

    #endregion
}
