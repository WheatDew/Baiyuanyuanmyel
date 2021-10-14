using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caipiao : MonoBehaviour
{
    public int shuzi;
    public int min,max;

    public void ShengChengShuZi()
    {
        shuzi = Random.Range(min, max);
    }
}
