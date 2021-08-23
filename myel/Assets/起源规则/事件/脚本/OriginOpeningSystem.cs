using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OriginOpeningSystem : MonoBehaviour
{
    public CameraCtrl cameraCtrl;
    public Text discribe;

    public void Start1()
    {
        cameraCtrl.displayLat = -23;
        cameraCtrl.displayLon = 20;
        cameraCtrl.SetPosition();
        SetText("以白元元为初始角色开始游戏，白元元是各个时空交汇处的观测者，基于这一点她可以在各个不同的时空旅行");
    }

    public void Start2()
    {
        cameraCtrl.displayLat = -30;
        cameraCtrl.displayLon = 30;
        cameraCtrl.SetPosition();
        SetText("未知起源");
    }

    public void StartGame()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            print(asyncLoad.progress);
            yield return null;
        }
    }

    public void SetText(string content)
    {
        discribe.text = content;
    }
}
