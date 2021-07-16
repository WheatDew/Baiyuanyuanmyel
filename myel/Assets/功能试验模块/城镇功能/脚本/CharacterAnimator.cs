using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    
    public MeshRenderer m_meshRenderer;
    public Rigidbody m_rigibody;
    public int currentStatu = 0;
    public int[] currentAnimation;
    public bool isPlaying;

    private void Start()
    {
        currentAnimation = new int[] { 0, 4, 8, 12 };
        StartCoroutine(PlayAnimation());
    }

    public void Update()
    {
        if (m_rigibody.velocity.z > 0)
            currentAnimation = new int[] { 1, 6, 11, 16 };
        if (m_rigibody.velocity.z < 0)
            currentAnimation = new int[] { 2, 7, 12, 17 };
        if (m_rigibody.velocity.x < 0)
            currentAnimation = new int[] { 3, 8, 13, 18 };
        if (m_rigibody.velocity.x > 0)
            currentAnimation = new int[] { 4, 9, 14, 19 };
        if (m_rigibody.velocity==Vector3.zero)
            currentAnimation = new int[] { 0, 5, 10, 15 };
    }

    public void SetCurrentStatu(int id)
    {
        m_meshRenderer.material.mainTextureOffset = new Vector2((id / 5)*0.25f, (id % 5)*0.2f);
    }

    IEnumerator PlayAnimation()
    {
        int currentAnimationID = 0;
        while (true)
        {
            if (isPlaying && currentAnimation.Length != 0)
            {
                SetCurrentStatu(currentAnimation[currentAnimationID]);
                currentAnimationID++;
                if (currentAnimationID > currentAnimation.Length - 1)
                    currentAnimationID = 0;
            }
            
            yield return new WaitForSeconds(0.25f);
        }
    }
}
