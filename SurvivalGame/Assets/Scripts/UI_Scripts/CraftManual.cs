using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName; // �̸�
    public GameObject go_Prefab; // ���� ��ġ�� ������
    public GameObject go_PreviewPrefab; // �̸����� ������
}

public class CraftManual : MonoBehaviour
{
    // ���º���
    bool isActivated;
    bool isPreviewActivated;

    [SerializeField]
    GameObject go_BaseUI; // �⺻ ���̽� UI

    [SerializeField]
    Craft[] craft_fire; // ��ںҿ� ��

    GameObject go_Preview; // �̸����� �������� ���� ����
    GameObject go_Prefab; // ���� ������ �������� ���� ����

    [SerializeField]
    Transform tf_Player; // �÷��̾� ��ġ

    // Raycast �ʿ� ���� ����
    RaycastHit hitinfo;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    float range;

    public void SlotClick(int _slotNumber)
    {
        go_Preview = Instantiate(craft_fire[_slotNumber].go_PreviewPrefab, tf_Player.position + tf_Player.transform.forward, Quaternion.identity);
        go_Prefab = craft_fire[_slotNumber].go_Prefab; 
        isPreviewActivated = true;
        CloseWindow();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
        {
            Window();
        }

        if (isPreviewActivated)
        {
            PreviewPositionUpdate();

            if (Input.GetButtonDown("Fire1"))
            {
                Build();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    void Build()
    {
        Instantiate(go_Prefab, hitinfo.point, Quaternion.identity);
        Destroy(go_Preview);
        Cancel();
    }


    void PreviewPositionUpdate()
    {
        if(Physics.Raycast(tf_Player.position, tf_Player.forward, out hitinfo, range, layerMask))
        {
            if(hitinfo.transform != null)
            {
                Vector3 _location = hitinfo.point;
                go_Preview.transform.position = _location;
            }
        }
    }

    void Cancel()
    {
        if (isPreviewActivated)
        {
            Destroy(go_Preview);
        }
        
        isPreviewActivated = false;
        go_Preview = null;
        go_Prefab = null;
    }

    void Window()
    {
        if (!isActivated)
        {
            OpenWindow();
        }
        else
        {
            CloseWindow();
        }
    }

    void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
    }
}
