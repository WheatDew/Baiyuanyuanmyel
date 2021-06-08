using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum PlayerDir { forward,back,left,right};
public class PlayerSelfMove : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
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
        switch (direction)
        {
            case PlayerDir.forward:
                playerCamera.transform.position += Vector3.forward * Time.deltaTime;
                break;
            case PlayerDir.back:
                playerCamera.transform.position += Vector3.back * Time.deltaTime;
                break;
            case PlayerDir.left:
                playerCamera.transform.position += Vector3.left * Time.deltaTime;
                break;
            case PlayerDir.right:
                playerCamera.transform.position += Vector3.right * Time.deltaTime;
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
