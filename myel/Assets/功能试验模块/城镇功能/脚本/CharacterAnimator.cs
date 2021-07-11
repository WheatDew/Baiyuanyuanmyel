using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public MeshRenderer m_meshRenderer;
    private CharacterTextureList characterTextureList;
    public int currentTexture = 0;

    private void Start()
    {
        characterTextureList = FindObjectOfType<CharacterTextureList>();
        m_meshRenderer.material.mainTexture = characterTextureList.textureList[currentTexture];
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateCurrentTexture(currentTexture);
        }
    }

    public void UpdateCurrentTexture(int textureID)
    {
        m_meshRenderer.material.mainTexture = characterTextureList.textureList[textureID];
    }
}
