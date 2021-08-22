using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OriginOpeningSystem : MonoBehaviour
{
    public CameraCtrl cameraCtrl;

    public void Start1()
    {
        cameraCtrl.displayLat = -23;
        cameraCtrl.displayLon = 20;
        cameraCtrl.SetPosition();
    }

    public void Start2()
    {
        cameraCtrl.displayLat = -30;
        cameraCtrl.displayLon = 30;
        cameraCtrl.SetPosition();
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
}
