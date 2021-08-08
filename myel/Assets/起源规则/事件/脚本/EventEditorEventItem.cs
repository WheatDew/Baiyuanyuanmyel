using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventEditorEventItem : MonoBehaviour
{

    private Button selfButton;
    public Text buttonText;
    public InputField inputField;
    public string eventName;
    public string eventDescribe;
    public List<EventButtonData> eventButtonDatas;
    // Start is called before the first frame update
    void Start()
    {
        selfButton = GetComponent<Button>();
        selfButton.onClick.AddListener(delegate
        {
            inputField.gameObject.SetActive(true);
        });
        inputField.onEndEdit.AddListener(delegate
        {
            buttonText.text = inputField.text;
            inputField.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
