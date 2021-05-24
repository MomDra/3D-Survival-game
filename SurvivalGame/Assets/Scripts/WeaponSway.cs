using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    // 기존 위치
    Vector3 originPos;

    // 현재 위치
    Vector3 currentPos;

    // Sway 한계
    [SerializeField]
    Vector3 limitPos;

    // 정조준 Sway 한계
    [SerializeField]
    Vector3 fineSightLimitPos;

    // 부드러운 움직임 정도
    [SerializeField]
    Vector3 smoothSway;

    // 필요한 컴포넌트
    [SerializeField]
    GunController theGunController;

    private void Start()
    {
        originPos = this.transform.localPosition;
    }

    private void Update()
    {
        TrySway();
    }

    void TrySway()
    {
        if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
            Swaying();
        else
            BackToOriginPos();
    }

    void Swaying()
    {
        float _moveX = Input.GetAxisRaw("Mouse X");
        float _moveY = Input.GetAxisRaw("Mouse Y");
        Vector3 limit;

        if (!theGunController.GetFineSightMode())
            limit = limitPos;
        else
            limit = fineSightLimitPos;
            
        smoothSway.x = limit.z;

        currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_moveX, smoothSway.x), -limit.x, limit.x),
                       Mathf.Clamp(Mathf.Lerp(currentPos.y, -_moveX, smoothSway.y), -limit.y, limit.y),
                       originPos.z);


        transform.localPosition = currentPos;
    }

    void BackToOriginPos()
    {
        currentPos = Vector3.Lerp(currentPos, originPos, smoothSway.x);
        transform.localPosition = currentPos;
    }
}
