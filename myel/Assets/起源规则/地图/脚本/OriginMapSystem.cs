using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginMapSystem : MonoBehaviour
{
    private OriginCharacterSelectionSystem characterSelectionSystem;
    public Dictionary<string, Vector3> mapList = new Dictionary<string, Vector3>();

    private void Start()
    {
        characterSelectionSystem = FindObjectOfType<OriginCharacterSelectionSystem>();
        mapList.Add("路灯", new Vector3(2000, -2000, 0));
        mapList.Add("路灯2", Vector3.zero);

        //在这里添加
        mapList.Add("曙光_平民_仓库入口", new Vector3(2014.8f, 0.1292453f, 89.91778f));
        mapList.Add("曙光_平民_仓库出口", new Vector3(75.60001f, 8, 66.9f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetNewScene("路灯");
        }
    }

    public void SetNewScene(string mapName)
    {
        print(mapName);
        if (mapList.ContainsKey(mapName))
        {
            Camera.main.transform.parent.position = Vector3.up*2.4f + mapList[mapName];
            
            GameObject obj = Instantiate(characterSelectionSystem.targetCharacter.gameObject);

            Destroy(characterSelectionSystem.targetCharacter.gameObject);

            characterSelectionSystem.targetCharacter = obj.GetComponent<OriginCharacter>();
            obj.transform.position =  mapList[mapName];
        }
    }
}
