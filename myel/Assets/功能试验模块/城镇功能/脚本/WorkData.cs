using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkData
{
    public float time=10;
    public Item[] cost;
    public Item[] gain;
}

public struct Item
{
    public string name;
    public int count;

}

public struct Status
{
    public float value;
    public float rate;

    public void ValueGain(float gain)
    {
        value += gain;
    }
    public void RateGain(float gain)
    {
        rate += gain;
    }
};
