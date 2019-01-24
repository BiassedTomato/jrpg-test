using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    public GameObject LoadScreen;

    public void StartGame()
    {
        StartCoroutine("Load");
    }

    IEnumerator Load()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync("SampleScene");
       // loadScene.allowSceneActivation = false;

        LoadScreen.SetActive(true);

        //yield return new WaitForSeconds(5);
        yield return new WaitUntil(() => loadScene.isDone);
        //loadScene.allowSceneActivation = true;
    }

}
