using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string sceneName = "TitleScenes";
    private static bool isMenu;
    [SerializeField]
    private GameObject menuActive;
    [SerializeField]
    private SaveNLoad theSaveNLoad;
    //[SerializeField]
    //private SaveNLoad

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenu)
                OpenMenu();
            else
                CloseMenu();
        }
    }
    private void OpenMenu()
    {
        isMenu = true;
        menuActive.SetActive(true);
    }
    private void CloseMenu()
    {
        isMenu = false;
        menuActive.SetActive(false);
    }
    public void ClickSave()
    {
        theSaveNLoad.SaveData();
    }
    public void ClickLoad()
    {
        theSaveNLoad.LoadData();
    }
    public void ClickExit()
    {
        SceneManager.LoadScene(sceneName);
    }
}
