using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public CharacterNavigation characterNavigation;
    private CharacterSelectionController selectionController;
    public MeshRenderer m_meshRenderer;
    public Rigidbody m_rigidbody;
    public int[] animationArray;
    public bool isNav=true;

    public void Start()
    {
        selectionController = FindObjectOfType<CharacterSelectionController>();
        StartCoroutine(PlayAnimation());
    }

    public void Update()
    {
        

        if((selectionController.character == null || selectionController.character != gameObject))
        {
            isNav = true;
            if (Mathf.Abs(characterNavigation.targetPosition.x - transform.position.x)
            < Mathf.Abs(characterNavigation.targetPosition.z - transform.position.z))
            {
                if (characterNavigation.targetPosition.z - transform.position.z > 0)
                    animationArray = new int[] { 1, 6, 11, 16 };
                if (characterNavigation.targetPosition.z - transform.position.z < 0)
                    animationArray = new int[] { 2, 7, 12, 17 };
            }
            else
            {
                if (characterNavigation.targetPosition.x - transform.position.x < 0)
                    animationArray = new int[] { 3, 8, 13, 18 };
                if (characterNavigation.targetPosition.x - transform.position.x > 0)
                    animationArray = new int[] { 4, 9, 14, 19 };
            }


            if (Mathf.Abs(characterNavigation.targetPosition.x - transform.position.x) < 0.05f
                && Mathf.Abs(characterNavigation.targetPosition.z - transform.position.z) < 0.05f)
                animationArray = new int[] { 0, 5, 10, 15 };
        }
        else
        {
            isNav = false;
            if (Mathf.Abs(m_rigidbody.velocity.x) < Mathf.Abs(m_rigidbody.velocity.z))
            {
                if (m_rigidbody.velocity.z > 0)
                    animationArray = new int[] { 1, 6, 11, 16 };
                if (m_rigidbody.velocity.z < 0)
                    animationArray = new int[] { 2, 7, 12, 17 };
            }
            else
            {
                if (m_rigidbody.velocity.x < 0)
                    animationArray = new int[] { 3, 8, 13, 18 };
                if (m_rigidbody.velocity.x > 0)
                    animationArray = new int[] { 4, 9, 14, 19 };
            }


            if (Mathf.Abs(m_rigidbody.velocity.x)<0.05f&& Mathf.Abs(m_rigidbody.velocity.z)<0.05f)
                animationArray = new int[] { 0, 5, 10, 15 };
        }

        
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
        m_meshRenderer.material.mainTextureOffset = new Vector2((id / 5) * 0.25f, (id % 5) * 0.2f);
    }
}
