using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAnimation : MonoBehaviour
{
    public MeshRenderer m_meshRenderer;
    public int[] animationArray;
    public int row=1, column=1;

    public void Start()
    {
        StartCoroutine(PlayAnimation());
    }


    IEnumerator PlayAnimation()
    {
        int currentID = 0;
        while (true)
        {
            if (animationArray.Length != 0)
            {

                SetCharacterTexture(animationArray[currentID]);
                currentID++;
                if (currentID > 3)
                    currentID = 0;

            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void SetCharacterTexture(int id)
    {
        m_meshRenderer.material.mainTextureOffset = new Vector2((id / 5) * 1f/(float)row, (id % 5) * 1f/(float)column);
    }
}
