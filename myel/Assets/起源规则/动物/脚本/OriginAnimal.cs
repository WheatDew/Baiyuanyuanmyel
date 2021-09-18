using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginAnimal : MonoBehaviour
{
    public Sprite animation;
    private Material material;
    private int index = -999;
    private int indexMax=1;
    private float animationTimer = 0;

    private Rigidbody m_rigidbody;

    public Transform target;


    private void Start()
    {
        material = FindObjectOfType<MeshRenderer>().material;
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            index = 0;
        }

        AnimationJob();

        PathFindingJob();

        AnimationDirectionJob();
    }

    //设置动画图片索引
    public void AnimationJob()
    {
        if (index != -999)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer>0.25f)
            {
                if (index > -1)
                {
                    if (index < indexMax)
                    {
                        material.mainTextureOffset = new Vector2(1/(float)indexMax*index, 0);
                        index += 1;
                    }
                    else
                        index = 0;
                }
                else if (index == -1)
                {
                    material.mainTextureOffset = new Vector2(0, 0);
                    index = -999;
                }
                animationTimer = 0;
            }
        }
    }

    //设置动物寻路
    public void PathFindingJob()
    {
        Vector3 p = target.position-transform.position;
        p.y = 0;
        m_rigidbody.AddForce(p, ForceMode.Acceleration);
    }

    //设置图片方向
    public void AnimationDirectionJob()
    {
        if (m_rigidbody.velocity.x >= 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
