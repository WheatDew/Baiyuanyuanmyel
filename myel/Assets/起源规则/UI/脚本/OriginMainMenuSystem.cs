using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginMainMenuSystem : MonoBehaviour
{
    [SerializeField] private OriginMainMenuGuide mainMenuGuidePrefab;
    [System.NonSerialized] public OriginMainMenuGuide mainMenuGuide;


    //外部系统的引用
    private OriginPackSystem packSystem;

    private void Start()
    {
        mainMenuGuide = Instantiate(mainMenuGuidePrefab, FindObjectOfType<Canvas>().transform);
        packSystem = FindObjectOfType<OriginPackSystem>();
    }

    public void DisplayPackPage()
    {
        packSystem.OpenPackPage();

    }
}
