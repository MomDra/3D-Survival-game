using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject go_BaseUI;
    [SerializeField]
    SaveAndLoad theSaveNLoad;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!GameManager.isPause)
            {
                CallMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }

    void CallMenu()
    {
        GameManager.isPause = true;
        go_BaseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void CloseMenu()
    {
        GameManager.isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ClickSave()
    {
        Debug.Log("세이브 버튼 클릭");
        theSaveNLoad.SaveData();
    }

    public void ClickLoad()
    {
        Debug.Log("로드 버튼 클릭");
        theSaveNLoad.LoadData();
    }

    public void ClickExit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }
}
