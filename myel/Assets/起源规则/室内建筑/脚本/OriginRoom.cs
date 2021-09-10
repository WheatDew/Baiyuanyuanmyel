using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginRoom : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public GameObject colliderObj;
    private OriginCharacterSystem characterSystem;
    public Vector3 offset;

    public void SetRoom(Texture paint)
    {
        meshRenderer.material.mainTexture = paint;

        characterSystem = FindObjectOfType<OriginCharacterSystem>();
        Vector3 pos = offset + transform.position;
        Stack<Command> commands = new Stack<Command>();
        commands.Push(new Command((int)SystemType.Room, (int)EventCommand.Create, 
            string.Format("{0},{1},{2},{3},{4}", colliderObj.name, colliderObj.name,pos.x,pos.y,pos.z)));
        CharacterActionButton cab = new CharacterActionButton("出入口", commands);
        characterSystem.characterActionButton.Add("出入口", cab);
    }
}
