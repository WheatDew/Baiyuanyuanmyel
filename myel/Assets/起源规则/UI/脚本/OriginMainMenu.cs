using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginMainMenu : MonoBehaviour
{

    //外部系统的引用
    private OriginPackSystem packSystem;
    private OriginPhysiologyPropertySystem physiologyPropertySystem;

    private void Start()
    {
        packSystem = FindObjectOfType<OriginPackSystem>();
        physiologyPropertySystem = FindObjectOfType<OriginPhysiologyPropertySystem>();
    }

    public void DisplayPackPage()
    {
        packSystem.OpenPackPage();
        Destroy(gameObject);
    }

    public void DisplayPhysiologyPropertyPage()
    {
        physiologyPropertySystem.OpenPhysiologyPropertyPage();
        Destroy(gameObject);
    }
}
