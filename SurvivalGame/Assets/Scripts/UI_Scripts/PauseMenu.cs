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
        Debug.Log("���̺� ��ư Ŭ��");
        theSaveNLoad.SaveData();
    }

    public void ClickLoad()
    {
        Debug.Log("�ε� ��ư Ŭ��");
        theSaveNLoad.LoadData();
    }

    public void ClickExit()
    {
        Debug.Log("��������");
        Application.Quit();
    }
}
