using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName; // 이름
    public GameObject go_Prefab; // 실제 설치될 프리팹
    public GameObject go_PreviewPrefab; // 미리보기 프리팹
}

public class CraftManual : MonoBehaviour
{
    // 상태변수
    bool isActivated;
    bool isPreviewActivated;

    [SerializeField]
    GameObject go_BaseUI; // 기본 베이스 UI

    [SerializeField]
    Craft[] craft_fire; // 모닥불용 탭

    GameObject go_Preview; // 미리보기 프리팹을 담을 변수
    GameObject go_Prefab; // 실제 생성될 프리팹을 담을 변수

    [SerializeField]
    Transform tf_Player; // 플레이어 위치

    // Raycast 필요 변수 선언
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
