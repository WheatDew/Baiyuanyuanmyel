using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum PlayerDir { forward,back,left,right};
public class PlayerSelfMove : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public float speedValue; 
    public PlayerDir direction;
    private GameObject playerCamera;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main.gameObject;
        
    }

    private void Update()
    {
        if(flag&&Input.GetMouseButton(0))
        Move();
    }

    public void Move()
    {
	Quaternion target= playerCamera.transform.rotation;
       	 target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
        switch (direction)
        {
            case PlayerDir.forward:
                playerCamera.transform.localPosition += target * Vector3.forward * Time.deltaTime * speedValue;
                break;
            case PlayerDir.back:
                playerCamera.transform.localPosition += target * Vector3.back * Time.deltaTime * speedValue;
                break;
            case PlayerDir.left:
                playerCamera.transform.localPosition += target * Vector3.left * Time.deltaTime * speedValue;
                break;
            case PlayerDir.right:
                playerCamera.transform.localPosition += target * Vector3.right * Time.deltaTime * speedValue;
                break;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        flag = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        flag = false;
    }
}
