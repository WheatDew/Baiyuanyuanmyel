using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWork : MonoBehaviour
{
    public CharacterValueStatus characterValueStatus;
    public string currentWork;
    public Dictionary<string, WorkData> Recipes = new Dictionary<string, WorkData>();
    public float timer=0;

    private void Start()
    {
        Recipes.Add("摸鱼", new WorkData { cost=new Item[0], gain = new Item[1] { new Item { name = "鱼", count = 1 } }, time = 5 });
        Recipes.Add("取水", new WorkData { cost = new Item[0], gain = new Item[1] { new Item { name = "水", count = 1 } }, time = 5 });
    }

    private void Update()
    {
        if (currentWork != ""&&Recipes.ContainsKey(currentWork))
        {
            timer += Time.deltaTime;
            if (timer > Recipes[currentWork].time)
            {
                foreach(var item in Recipes[currentWork].cost)
                {
                    characterValueStatus.command.Push(string.Format("背包增加,{0},{1},",item.name,-item.count));
                }
                foreach (var item in Recipes[currentWork].gain)
                {
                    characterValueStatus.command.Push(string.Format("背包增加,{0},{1},", item.name, item.count));
                }
                timer = 0;
            }
        }
        else
        {
            timer = 0;
        }
    }

    //IEnumerator StartWork()
    //{
    //    while (currentWork!="")
    //    {
    //        yield return new WaitForSeconds(Recipes[currentWork].time);

    //        foreach(var item in currentWork)
    //        {

    //        }
    //    }

    //    yield return null;
    //}

}
