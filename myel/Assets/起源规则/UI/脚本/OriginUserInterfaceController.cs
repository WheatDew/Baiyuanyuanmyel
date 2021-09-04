using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class OriginUserInterfaceController : MonoBehaviour
{
    //通用功能
    private Canvas canvas;

    //对话框
    [SerializeField] private OriginDialogueBox dialoguePrefab;
    [System.NonSerialized] public OriginDialogueBox dialogueBox;
    private float reportTimer = 0;

    //通用继承
    private void Start()
    {
        GeneralInitialize();
        DialogueInitialize();
        ClickDialogueBox();
    }

    private void Update()
    {
        DialogueJob();
    }

    //通用功能模块
    private void GeneralInitialize()
    {
        canvas = FindObjectOfType<Canvas>();
    }


    #region 对话框模块

    /// <summary>
    /// 对话框调用
    /// </summary>
    public void CreateDialogueBox()
    {

    }

    //点击式对话框
    public void ClickDialogueBox()
    {
        Stack<string> ss = new Stack<string>();
        ss.Push("aa");
        ss.Push("bb");
        StartCoroutine(ClickDialogueBoxCoroutine(ss));
    }
    //点击式对话框协程
    public IEnumerator ClickDialogueBoxCoroutine(Stack<string> dialogueData)
    {
        while (true)
        {
            if(EventSystem.current.firstSelectedGameObject!=null)
            print(EventSystem.current.firstSelectedGameObject.name);

            yield return null;
        }
    }

    //初始化对话框
    private void DialogueInitialize()
    {
        dialogueBox = Instantiate(dialoguePrefab, canvas.transform);
        dialogueBox.gameObject.SetActive(false);
    }

    //激活播报对话框
    public void ActivateReportDialogue(string content)
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.SetContent(content);
        reportTimer = 3;
    }
    //语音播报循环
    private void ReportDialogueTimerJob()
    {
        if (reportTimer!=999&&reportTimer > 0)
        {
            reportTimer -= Time.deltaTime;
        }

        if (reportTimer <= 0)
        {
            ClosedDialogue();
            reportTimer = 999;
        }
    }

    //关闭对话框
    public void ClosedDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
    }

    //对话框循环
    public void DialogueJob()
    {
        ReportDialogueTimerJob();
    }
    #endregion
}
