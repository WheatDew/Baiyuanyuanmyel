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
        if (mapList.ContainsKey(mapName))
        {
            print(mapName);
            Camera.main.transform.parent.position = Vector3.up*2.4f + mapList[mapName];
            
            GameObject obj = Instantiate(characterSelectionSystem.targetCharacter.gameObject);

            Destroy(characterSelectionSystem.targetCharacter.gameObject);
            obj.transform.position =  mapList[mapName];
        }
    }
}
