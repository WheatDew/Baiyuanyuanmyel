using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginWorkBubble : MonoBehaviour
{
    public SpriteRenderer content;
    public SpriteRenderer background;
    [System.NonSerialized] public OriginCharacter originCharacter;
    

    private OriginCommandSystem commandSystem;
    private Command command;

    public void SetContent(Sprite texture, Command command,OriginCommandSystem commandSystem)
    {
        content.sprite = texture;
        this.commandSystem = commandSystem;
        this.command = command;
    }

    //延迟执行函数
    IEnumerator Process()
    {
        background.color = new Color(1, 1, 0);
        float rate = 0;
        while (rate < 1)
        {
            background.color = new Color(1 - rate, 1, 0);
                
            rate += Time.deltaTime * (1f / 3f);
            yield return null;
        }
        background.color = new Color(1, 1, 1);
        commandSystem.PushCommand(command);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && command != null)
        {
            RaycastHit result;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out result)&&result.collider.gameObject==gameObject)
            {
                StopAllCoroutines();
                StartCoroutine(Process());
            }
            
        }
    }

}
