using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneNext = "YosiLand";
    public static Title instans;
    private SaveNLoad theSaveNLoad;
    private void Awake()
    {
        if (instans == null)
        {
            instans = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);

    }
    public void ClickStart()
    {
        SceneManager.LoadScene(sceneNext);
    }
    public void ClickLoad()
    {
        StartCoroutine(LoadCoroutine());
    }
    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNext);
        while (!operation.isDone)
        {
            yield return null;
        }
        theSaveNLoad = FindObjectOfType<SaveNLoad>();
        theSaveNLoad.LoadData();
        gameObject.SetActive(false);
    }
    public void ClickExit()
    {
        Application.Quit();
    }
}
