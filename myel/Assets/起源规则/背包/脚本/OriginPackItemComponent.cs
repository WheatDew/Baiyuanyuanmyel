using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OriginPackItemComponent : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public OriginEffectManager effectMananger;
    public Canvas canvas;
    [SerializeField] private OriginPackItemDescribeComponent describePerfab;
    [System.NonSerialized] public OriginPackItemDescribeComponent describeObj;
    public Image image;
    public Text count;
    public string describe;
    public Stack<EffectData> effectDatas=new Stack<EffectData>();

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            PushCommand();
        });
        
    }

    public void PushCommand()
    {
        while (effectDatas.Count != 0)
        {
            effectMananger.effectCommand.Push(effectDatas.Pop());
        }

    }

    public void DisplayDescribe()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        describeObj = Instantiate(describePerfab, canvas.transform);
        describeObj.describe.text = describe;
        describeObj.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(describeObj.gameObject);
    }
}
